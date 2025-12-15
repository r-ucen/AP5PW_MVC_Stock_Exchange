const LOCALE = "en-US";

function formatNumber(value) {
    return value.toLocaleString(LOCALE, {
        minimumFractionDigits: 2,
        maximumFractionDigits: 2
    });
}

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/stockHub")
    .withAutomaticReconnect()
    .build();

connection.on("ReceiveStockPriceUpdate", (tickerSymbol, newPrice) => {
    const priceElements = document.querySelectorAll(`[data-stock-price="${tickerSymbol}"]`);

    priceElements.forEach(element => {
        element.textContent = formatNumber(newPrice);
    });
});

connection.on("ReceivePortfolioSummaryUpdate", summary => {
    const root = document;
    const portfolioValueElements = root.querySelectorAll("[data-portfolio-value]");
    const unrealizedGainElements = root.querySelectorAll("[data-unrealized-gains]");
    const unrealizedGainPctElements = root.querySelectorAll("[data-unrealized-gains-pct]");
    const availableCashElements = root.querySelectorAll("[data-available-cash]");
    const depositsElements = root.querySelectorAll("[data-deposits]");

    portfolioValueElements.forEach(element => {
        element.textContent = formatNumber(summary.portfolioValue);
    });

    unrealizedGainElements.forEach(element => {
        element.textContent = formatNumber(summary.unrealizedGains);
        const gainsPainter = element.closest("[gains-painter]");

        if (gainsPainter) {
            gainsPainter.classList.toggle("text-success", summary.unrealizedGains > 0);
            gainsPainter.classList.toggle("text-muted", summary.unrealizedGains
                == 0);
            gainsPainter.classList.toggle("text-danger", summary.unrealizedGains < 0);
        }
    });

    unrealizedGainPctElements.forEach(element => {
        element.textContent = formatNumber(summary.unrealizedGainPercentage);
    });

    availableCashElements.forEach(element => {
        element.textContent = formatNumber(summary.availableCash);
    });

    depositsElements.forEach(element => {
        element.textContent = formatNumber(summary.deposits);
    })
});

connection.on("ReceivePortfolioHoldingsUpdate", holdings => {
    if (!Array.isArray(holdings)) return;

    holdings.forEach(holding => {
        const holdingRow = document.querySelector(`[data-stock-id="${holding.stockId}"]`);
        if (!holdingRow) return;

        const quantityEl = holdingRow.querySelector("[data-holding-quantity]");
        const totalValueEl = holdingRow.querySelector("[data-holding-total-value]");
        const gainLossEl = holdingRow.querySelector("[data-holding-gain-loss]");
        const gainLossPctEl = holdingRow.querySelector("[data-holding-gain-loss-pct]");
        const currentPriceEl = holdingRow.querySelector("[data-holding-current-price]");
        const gainLossPainterEl = holdingRow.querySelector("[data-gain-loss-painter]");

        if (quantityEl) quantityEl.textContent = holding.quantity;
        if (totalValueEl) totalValueEl.textContent = formatNumber(holding.totalValue);
        if (gainLossEl) gainLossEl.textContent = formatNumber(holding.gainLoss);
        if (currentPriceEl) currentPriceEl.textContent = formatNumber(holding.currentPrice);
        if (gainLossPctEl) gainLossPctEl.textContent = formatNumber(holding.gainLossPercentage);

        if (gainLossPainterEl) {
            gainLossPainterEl.classList.toggle("text-success", holding.gainLoss > 0);
            gainLossPainterEl.classList.toggle("text-muted", holding.gainLoss == 0);
            gainLossPainterEl.classList.toggle("text-danger", holding.gainLoss < 0);
        }
    });
});

async function start() {
        try {
        await connection.start();
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
}

start();