(() => {
    const URL = 'http://localhost:8080/shop';
    var ID;

    let getDetail = () => {
        $.ajax({
            url : `${URL}/detail/${ID}`,
            method: "GET",
            success : (response) => {
                console.log(response);
                openModalDetailInfo();
                bindingDataToModalInfo(response);
            },
        });
    };

    let getSelectListShipment = () => {
        $.ajax({
            url : `${URL}/selectlistshipments`,
            method: "GET",
            success : (response) => {
                // console.log(response);
                openModalAddCart();
                bindingListShippmentToModalAddCart(response);
            },
        });
    };

    let addToCart = (data) => {
        $.ajax({
            url : `${URL}/addcart/${ID}`,
            method: "POST",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(data),
            success : (response) => {
                closeModalAddCart();
                showAlert(response);
            },
            error : () => {
                showAlert("All field must be filled");
            }
        });
    };

    let bindingDataToModalInfo = (data) => {
        document.querySelector('#name').textContent = data.name;
        document.querySelector('#category').textContent = data.category;
        document.querySelector('#description').textContent = data.description;
        document.querySelector('#price').textContent = data.price;
        document.querySelector('#sellername').textContent = data.sellerName;
    };

    let bindingListShippmentToModalAddCart = (data) => {
        let selectListShipment = document.querySelector('select[name=shipment]');
        selectListShipment.innerHTML = '<option value="">Select Shipment</option>';
        data.forEach(shipment => {
            selectListShipment.innerHTML += `<option value=${shipment.value}>${shipment.text}</option>`
        });
    };

    let openModalDetailInfo = () => {
        document.querySelector('.modal-layer').style.display = 'flex';
        document.querySelector('.popup-dialog').style.display = 'block';
    };
    let openModalAddCart = () => {
        document.querySelector('.modal-addcart').style.display = 'flex';
        document.querySelector('.popup-addcart').style.display = 'block';
    };
    let showAlert = (message) => {
        document.querySelector('.modal-alert').style.display = 'flex';
        document.querySelector('.popup-alert').style.display = 'block';
        document.querySelector('#show-alert').textContent = message;
    };
    let showConfirm = (confirm, data) => {
        document.querySelector('.modal-confirm').style.display = 'flex';
        document.querySelector('.popup-confirm').style.display = 'block';
        document.querySelector('#show-confirm').textContent = confirm;
        document.querySelector('#yes-button').addEventListener('click', () => {
            closeModalConfirm();
            addToCart(data);
        });
        document.querySelector('#no-button').addEventListener('click', () => {
            closeModalConfirm();
        });
    };

    
    //trigger button detail / addcart
    document.querySelector('#data').addEventListener('click', (event) => {
        ID = event.target.getAttribute('id');
        let className = event.target.getAttribute('class');
        if(className == 'detail-info'){
            getDetail();
        }else if(className == 'add-cart'){
            getSelectListShipment();
        }
    });

    //trigger submit
    document.querySelector('#addtocart').addEventListener('submit', (event) => {
        event.preventDefault();
        let quantity = document.querySelector('input[name=quantity]').value;
        let shipment = document.querySelector('select[name=shipment]').value;
        let data = {
            Quantity: quantity,
            ShipmentId: shipment
        }
        showConfirm("Are you sure you want to add this product to cart?", data);
    });

    //close modal detail
    document.querySelector('#cancel-button').addEventListener('click', () => {
        document.querySelector('.modal-layer').style.display = 'none';
        document.querySelector('.popup-dialog').style.display = 'none';
    });
    let closeModalAddCart = () => {
        document.querySelector('.modal-addcart').style.display = 'none';
        document.querySelector('.popup-addcart').style.display = 'none';
    };
    //close modal add cart
    document.querySelector('.cancel-addcart').addEventListener('click', () => {
        closeModalAddCart();
    });
    document.querySelector('.close-alert').addEventListener('click', () => {
        document.querySelector('.modal-alert').style.display = 'none';
        document.querySelector('.popup-alert').style.display = 'none';
    });
    let closeModalConfirm = () => {
        document.querySelector('.modal-confirm').style.display = 'none';
        document.querySelector('.popup-confirm').style.display = 'none';
    };
})()