"use strict";

const IndexOfFirstElement = 0;
const IndexOfUrlId = 2;

var auctionId = parseInt(location.pathname.split('/')[IndexOfUrlId]);


document.getElementById("sendButton").disabled = true;

var connection = new signalR.HubConnectionBuilder().withUrl("/demoKaibayHub").build();
connection.start().then(() => {
    connection.invoke("AddToGroup", auctionId);
    document.getElementById("sendButton").disabled = false;
});

connection.on("UpdatePriceForGroup", function (auctionid, price) {
    $("#price-" + auctionid)[IndexOfFirstElement].innerHTML = price;
});

connection.on("UpdateNotification", function (price) {
    toastr["success"]("new bid for this auction " + price + " Euros");
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var price = parseFloat(document.getElementById("price").value);
    connection.invoke("BidRequest", auctionId, price)
        .catch(function (err) {
            Swal.fire({
                title: "Alert",
                text: err.toString(),
                type: "error"
            })
    });
    event.preventDefault();
});



$(window).bind('beforeunload', function () {
    connection.invoke("RemoveFromGroup", auctionId);
    connection.close();
});

connection.on("Reject", function (auctionid, message) {
    Swal.fire(
        'Alert',
        message,
        'error'
    );
});