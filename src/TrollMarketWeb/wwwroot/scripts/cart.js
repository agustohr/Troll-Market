(() => {
    const URL = 'http://localhost:8080/cart';
    var ID;

    let removeCart = () => {
        $.ajax({
            url : `${URL}/${ID}`,
            method: "DELETE",
            success : (response) => {
                showAlert(response);
                document.querySelector('.close-alert').addEventListener('click', () => {
                    location.reload();
                });
            },
            error : (response) => {
                showAlert(response.responseText);
            }
        });
    };

    let purchaseAllCart = () => {
        $.ajax({
            url : `${URL}/purchase`,
            method: "PUT",
            success : (response) => {
                showAlert(response);
                document.querySelector('.close-alert').addEventListener('click', () => {
                    location.reload();
                });
            },
            error : (response) => {
                showAlert(response.responseText);
            }
        });
    };

    let showConfirm = (confirm, action) => {
        document.querySelector('.modal-confirm').style.display = 'flex';
        document.querySelector('.popup-confirm').style.display = 'block';
        document.querySelector('#show-confirm').textContent = confirm;
        document.querySelector('#yes-button').addEventListener('click', () => {
            if(action == "remove"){
                closeModalConfirm();
                removeCart();
            }else if(action == "purchase"){
                closeModalConfirm();
                purchaseAllCart();
            }
        });
        document.querySelector('#no-button').addEventListener('click', () => {
            closeModalConfirm();
        });
    };

    let showAlert = (message) => {
        document.querySelector('.modal-alert').style.display = 'flex';
        document.querySelector('.popup-alert').style.display = 'block';
        document.querySelector('#show-alert').textContent = message;
    };

    document.querySelector('#purchase-button').addEventListener('click', () => {
        showConfirm("Are you sure you want to purchase all cart?", "purchase");

    });

    document.querySelector('#data').addEventListener('click', (event) => {
        ID = event.target.getAttribute('id');
        let className = event.target.getAttribute('class');

        if(className == 'remove'){
            showConfirm("Are you sure you want to remove this cart?", "remove");
        }
    });

    let closeModalConfirm = () => {
        document.querySelector('.modal-confirm').style.display = 'none';
        document.querySelector('.popup-confirm').style.display = 'none';
    };
    document.querySelector('.close-alert').addEventListener('click', () => {
        document.querySelector('.modal-alert').style.display = 'none';
        document.querySelector('.popup-alert').style.display = 'none';
    });
})()