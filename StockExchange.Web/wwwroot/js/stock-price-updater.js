const connection = new signalR.HubConnectionBuilder()
    .withUrl("/stockHub")
    .withAutomaticReconnect()
    .build();

connection.on("ReceiveStockPriceUpdate", (tickerSymbol, newPrice) => {
    const priceElements = document.querySelectorAll(`[data-stock-price="${tickerSymbol}"]`);

    priceElements.forEach(element => {
        element.innerText = newPrice.toFixed(2);
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