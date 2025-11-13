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

connection.on("RecievePortfolioSummaryUpdate", summary => {
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

async function start() {
        try {
        await connection.start();
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
}

start();