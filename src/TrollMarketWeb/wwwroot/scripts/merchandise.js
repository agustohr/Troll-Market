(() => {
    const URL = 'http://localhost:8080/merchandise';
    var ID;

    let showInfo = () => {
        $.ajax({
            url : `${URL}/info/${ID}`,
            method: "GET",
            success : (response) => {
                openModal();
                bindingDataToModal(response);
            },
        });
    };

    let setDiscontinue = () => {
        $.ajax({
            url : `${URL}/discontinue/${ID}`,
            method: "PATCH",
            success : (response) => {
                showAlert(response);
                document.querySelector('.close-alert').addEventListener('click', () => {
                    location.reload();
                });
            },
            error : (response) => {
                showAlert(response.responseText);
                document.querySelector('.close-alert').addEventListener('click', () => {
                    location.reload();
                });
            }
        });
    }

    let setDelete = () => {
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
                document.querySelector('.close-alert').addEventListener('click', () => {
                    location.reload();
                });
            }
        });
    }

    let bindingDataToModal = (data) => {
        document.querySelector('#name').textContent = data.name;
        document.querySelector('#category').textContent = data.category;
        document.querySelector('#description').textContent = data.description;
        document.querySelector('#price').textContent = data.priceRp;
        document.querySelector('#isdiscontinue').textContent = data.isDiscontinue ? "Yes" : "No";
    };

    let openModal = () => {
        document.querySelector('.modal-layer').style.display = 'flex';
        document.querySelector('.popup-dialog').style.display = 'block';
    };

    let showConfirm = (confirm, action) => {
        document.querySelector('.modal-confirm').style.display = 'flex';
        document.querySelector('.popup-confirm').style.display = 'block';
        document.querySelector('#show-confirm').textContent = confirm;
        document.querySelector('#yes-button').addEventListener('click', () => {
            if(action == "discontinue"){
                closeModalConfirm();
                setDiscontinue();
            }else if(action == "delete"){
                closeModalConfirm();
                setDelete();
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

    document.querySelector('#data').addEventListener('click', (event) => {
        ID = event.target.getAttribute('id');
        console.log(ID);
        let className = event.target.getAttribute('class');

        if(className == 'info'){
            showInfo();
        }else if(className == 'discontinue'){
            showConfirm("Are you sure you want to set discontinue this product?", "discontinue");
            // setDiscontinue();
        }else if(className == 'delete'){
            showConfirm("Are you sure you want to delete this product?", "delete");
            // let confirmDelete = confirm("Are you sure you want to delete this product?");
            // if(confirmDelete){
            //     setDelete();
            // }
        }
    });

    //close modal
    document.querySelector('#cancel-button').addEventListener('click', () => {
        document.querySelector('.modal-layer').style.display = 'none';
        document.querySelector('.popup-dialog').style.display = 'none';
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