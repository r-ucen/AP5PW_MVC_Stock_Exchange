const connection = new signalR.HubConnectionBuilder()
    .withUrl("/stockHub")
    .withAutomaticReconnect()
    .build();

connection.on("ReceiveStockPriceUpdate", (tickerSymbol, newPrice) => {
    const priceElements = document.querySelectorAll(`[data-stock-price="${tickerSymbol}"]`);

    priceElements.forEach(element => {
        element.textContent = newPrice.toFixed(2);
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
        element.textContent = summary.portfolioValue.toFixed(2);
    });

    unrealizedGainElements.forEach(element => {
        element.textContent = summary.unrealizedGains.toFixed(2);
        const gainsPainter = element.closest("[gains-painter]");

        if (gainsPainter) {
            gainsPainter.classList.toggle("text-success", summary.unrealizedGains > 0);
            gainsPainter.classList.toggle("text-white", summary.unrealizedGains === 0);
            gainsPainter.classList.toggle("text-danger", summary.unrealizedGains < 0);
        }
    });

    unrealizedGainPctElements.forEach(element => {
        element.textContent = summary.unrealizedGainPercentage.toFixed(2);
    });

    availableCashElements.forEach(element => {
        element.textContent = summary.availableCash.toFixed(2);
    });

    depositsElements.forEach(element => {
        element.textContent = summary.deposits.toFixed(2);
    })
});
/*
connection.on("ReceivePortfolioHoldingsUpdate", holdings => {
    if (!Array.isArray(holdings)) return;

    holdings.forEach(holding => {

        const root = document.querySelector(`[data-stock-id="${holding.stockId}"]`);
        if (!root) return;
        const quantityElements = root.querySelectorAll("[data-holding-quantity]");
        const totalValueElements = root.querySelectorAll("[data-holding-total-value]");
        const gainLossElements = root.querySelectorAll("[data-holding-gain-loss]");
        const gainLossPctElements = root.querySelectorAll("[data-holding-gain-loss-pct]");
        const currentPriceElements = root.querySelectorAll("[data-holding-current-price]");

        quantityElements.forEach(element => {
            element.textContent = holding.quantity;
        });

        totalValueElements.forEach(element => {
            element.textContent = holding.totalValue.toFixed(2);
        });

        gainLossElements.forEach(element => {
            element.textContent = holding.gainLoss.toFixed(2);
        });

        gainLossPctElements.forEach(element => {
            element.textContent = holding.gainLossPercentage.toFixed(2);

            gainLossPainterEl.classList.toggle("text-success", holding.gainLoss > 0);
            gainLossPainterEl.classList.toggle("text-muted", holding.gainLoss === 0);
            gainLossPainterEl.classList.toggle("text-danger", holding.gainLoss < 0);
        
        });

        currentPriceElements.forEach(element => {
            element.textContent = holding.currentPrice.toFixed(2);
        });
    });
})
*/

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
        if (totalValueEl) totalValueEl.textContent = holding.totalValue.toFixed(2);
        if (gainLossEl) gainLossEl.textContent = holding.gainLoss.toFixed(2);
        if (currentPriceEl) currentPriceEl.textContent = holding.currentPrice.toFixed(2);
        if (gainLossPctEl) gainLossPctEl.textContent = holding.gainLossPercentage.toFixed(2);

        if (gainLossPainterEl) {
            gainLossPainterEl.classList.toggle("text-success", holding.gainLoss > 0);
            gainLossPainterEl.classList.toggle("text-muted", holding.gainLoss === 0);
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