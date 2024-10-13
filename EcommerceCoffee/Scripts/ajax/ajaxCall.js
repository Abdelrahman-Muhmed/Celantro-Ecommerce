




//start Call Delete End Point
$(document).ready(function () {
    $(".deleteBtn").click(function () {
        var Id = $(this).data('id');
        var controller = $(this).data('controller');
        var type = $(this).data('method');

        // Trigger SweetAlert confirmation
        Swal.fire({
            title: "Are you sure?",
            text: "You won't be able to revert this!",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Yes, delete it!"
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    type: type,
                    url: '/' + controller + '/Delete/' + Id,
                    success: function (response) {
                        if (response.success) {
                            Swal.fire({
                                title: "Deleted!",
                                text: "Your item has been deleted.",
                                icon: "success"
                            }).then(() => {
                                location.reload(); // Corrected reload function
                            });
                        } else {
                            Swal.fire({
                                title: "Error!",
                                text: "An error occurred while deleting the item>>>>>.",
                                icon: "error"
                            });
                        }
                    },
                    error: function () {
                        // Show error message if AJAX fails
                        Swal.fire({
                            title: "Error!",
                            text: "An error occurred while deleting the item.",
                            icon: "error"
                        });
                    }
                });
            }
        });
    });
});

//End Call Delete End Point


//Start Searching
$(document).ready(function () {
    function filter(query) {
        var convertLowerCase = query.toLowerCase();
        //console.log("Filtering with query:", convertLowerCase); 

        // Iterate through each product item
        $(".card").each(function () {
            var itemName = $(this).data('name').toLowerCase();

            if (itemName.includes(convertLowerCase)) {
                $(this).show();
            }
            else {
                $(this).hide();

            }
        })

    }

    // Event listener for keyup on the search box
    $('#searchBox').on('keyup', function () {
        var query = $(this).val().trim();
        filter(query);
    });
})

//End Searching


//Start Add To Cart
$(document).ready(function () {

    // Event listener for Add to Cart button
    $(".addCartBtn").click(function () {
        //e.preventDefault(); // Prevent default anchor behavior

        var productId = $(this).data('id'); // Get the product ID from the data attribute

        $.ajax({
            type: "POST",
            url: '/Product/AddToCart/' + productId, // Call the correct endpoint
            //data: { id: productId },    // Send the product ID to the server
            success: function (response) {
          

                    console.log("Right");
                    Swal.fire({
                        title: "Success!",
                        text: response.message,
                        icon: "success",
                        confirmButtonText: "OK"
                    });
                 
                //} else {
                //    Swal.fire({
                //        title: "Error!",
                //        text: response.message,
                //        icon: "error",
                //        confirmButtonText: "OK"
                //    });
                //    console.log("Error");

                //}
            },
            error: function () {
                console.log("Error");

                Swal.fire({
                    title: "Error!",
                    text: "Failed to add product to cart",
                    icon: "error",
                    confirmButtonText: "OK"
                });
            }
        });
    });

   
});

//End Add To Cart

//Start Handle Order
$(document).ready(function () {
    function updateTotal() {
        let total = 0;

        // Iterate through each row to update the total for each item
        $('#order-items tr').each(function () {
            let priceText = $(this).find('.item-price').text().replace(/[^0-9.-]+/g, ''); // Remove currency symbols
            let price = parseFloat(priceText);

            let quantity = parseInt($(this).find('.item-quantity').val());

            if (!isNaN(price) && !isNaN(quantity)) {
                let itemTotal = price * quantity;

                // Update item total in the row
                $(this).find('.item-total').text(itemTotal.toLocaleString('en-US', { style: 'currency', currency: 'USD' }));

                // Add to the total order amount
                total += itemTotal;
            }
        });

        // Apply discount if any
        let discount = parseFloat($('#discount').val());
        if (!isNaN(discount) && discount > 0) {
            total = total - (total * (discount / 100));
        }

        // Update total amount in the UI
        $('#totalAmount').text(total.toLocaleString('en-US', { style: 'currency', currency: 'USD' }));

        return total; // Return the updated total amount
    }

    // Increment quantity
    $('.increment').click(function () {
        let quantityInput = $(this).siblings('.item-quantity');
        let currentQuantity = parseInt(quantityInput.val());
        quantityInput.val(currentQuantity + 1);
        updateTotal();
    });

    // Decrement quantity
    $('.decrement').click(function () {
        let quantityInput = $(this).siblings('.item-quantity');
        let currentQuantity = parseInt(quantityInput.val());
        if (currentQuantity > 1) {
            quantityInput.val(currentQuantity - 1);
            updateTotal();
        }
    });

    // Apply discount and update total
    $('#discount').on('input', function () {
        updateTotal();
    });

    // Create Order when button is clicked
    $('#createOrder').click(function () {
        let orderItems = [];
        $('#order-items tr').each(function () {
            let productName = $(this).find('td:first').text();
            let price = parseFloat($(this).find('.item-price').text().replace(/[^0-9.-]+/g, ''));
            let quantity = parseInt($(this).find('.item-quantity').val());

            if (!isNaN(price) && !isNaN(quantity)) {
                orderItems.push({
                    ProductName: productName,
                    Price: price,
                    Quantity: quantity
                });
            }
        });

        let discount = parseFloat($('#discount').val());
        if (isNaN(discount)) {
            discount = 0;
        }

        let totalAmount = updateTotal(); // Get the final total amount

        // Create the order object
        let orderData = {
            OrderItems: orderItems,
            Discount: discount,
            TotalAmount: updateTotal() // Include the updated total amount
        };

        // Send the order data to the server
        $.ajax({
            url: '/Basket/CreateOrder', // Change this to your actual POST URL
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(orderData),
            success: function (response) {
                // Handle success (e.g., show confirmation message or redirect)
                Swal.fire({
                    position: "top-end",
                    icon: "success",
                    title: "Your work has been saved",
                    showConfirmButton: false,
                    timer: 1500
                });
                window.location.href = '/Product/Index'; // Redirect to order confirmation page
            },
            error: function (error) {
                // Handle error
                alert('An error occurred while creating the order. Please try again.');
            }
        });
    });

    // Initialize total amount on page load
    updateTotal();
});

//End Handle Order












//Start Alert When Activity Time End

//$(document).ready(function () {

//    var inActivityTime = 110000; //1 minute and 50 seconds
//    var warningTimer;


//    //  reset inactivity timers

//    function resetInactivityTimer() {
//        clearTimeout(warningTimer);
//        startInactivityTimer();
//    }

//    // Start inactivity timer
//    function startInactivityTimer() {
//        warningTimer = setTimeout(showInactivityWarning, inActivityTime); // Show warning after inactivity time
//    }

//    // Show SweetAlert for inactivity warning

//    function showInactivityWarning() {
//        Swal.fire({
//            title: "Do you want to continue?",
//            text: "You have been inactive for a while. Do you want to stay logged in?",
//            icon: "question",
//            showCancelButton: true,
//            confirmButtonText: "Yes",
//            cancelButtonText: "No",
//            showCloseButton: true
//        }).then((result) => {
//            if (result.isConfirmed) {
//                // if confirm yes i will make rest for time
//                resetInactivityTimer();
//            }
//            else if (result.isDismissed) {
//                $.ajax({
//                    type: "POST",
//                    url: "/Account/Logout",
//                    success() {

//                        window.location.href = '/Account/Logout';
//                        console.log("Done With Logout");

//                    },
//                    error: function () {
//                        console.log("Error With Logout");
//                    }
//                })
//            }
//        })



//    }

//    // Reset the inactivity timer on any activity
//    $(document).on('mousemove keypress', function () {
//        resetInactivityTimer();
//    });

//    // Start the initial inactivity timer
//    startInactivityTimer();
//})

//End Alert When Acticity Time End


















