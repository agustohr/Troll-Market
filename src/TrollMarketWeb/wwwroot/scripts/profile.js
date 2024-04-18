(() => {
    const URL = 'http://localhost:8080/profile';

    let showAlert = (message) => {
        document.querySelector('.modal-alert').style.display = 'flex';
        document.querySelector('.popup-alert').style.display = 'block';
        document.querySelector('#show-alert').textContent = message;
    };

    document.querySelector('#add-balance').addEventListener('click', () => {
        document.querySelector('.modal-layer').style.display = 'flex';
        document.querySelector('.popup-dialog').style.display = 'block';
    });

    document.querySelector('#cancel-button').addEventListener('click', () => {
        document.querySelector('.modal-layer').style.display = 'none';
        document.querySelector('.popup-dialog').style.display = 'none';
    });

    document.querySelector('#topup').addEventListener('submit', (event) => {
        event.preventDefault();
        let balance = document.querySelector('input[name=balance]').value;
        console.log(balance);
        console.log(parseFloat(balance));
        if(balance == ''){
            showAlert("Balance cannot be 0");
        }else{
            var data = {
                Balance: parseFloat(balance)
            }
            $.ajax({
                url : `${URL}`,
                method: "PATCH",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(data),
                success : (response) => {
                    // closeModalAddCart();
                    showAlert(response);
                    document.querySelector('.close-alert').addEventListener('click', () => {
                        location.reload();
                    });
                },
                error : (response) => {
                    console.log(response);
                    showAlert(response.responseText);
                }
            });
        }
    });

    document.querySelector('.close-alert').addEventListener('click', () => {
        document.querySelector('.modal-alert').style.display = 'none';
        document.querySelector('.popup-alert').style.display = 'none';
    });

})()