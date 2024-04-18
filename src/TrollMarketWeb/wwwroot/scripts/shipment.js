(() => {
    const URL = 'http://localhost:8080/shipment';
    var ID;

    let getShipmentByID = () => {
        $.ajax({
            url : `${URL}/${ID}`,
            method: "GET",
            success : (response) => {
                openModalUpsert('UPDATE SHIPMENT');
                bindingDataToModalUpsert(JSON.parse(response));
            },
        });
    }

    let insertShipment = (data) => {
        $.ajax({
            url : URL,
            method: "POST",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(data),
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

    let updateShipment = (data) => {
        $.ajax({
            url : `${URL}/${ID}`,
            method: "PUT",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(data),
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
    
    let stopService = () => {
        $.ajax({
            url : `${URL}/${ID}`,
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

    let deleteShipment = () => {
        if(confirmDelete){
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
                    showAlert(response.responseText)
                    document.querySelector('.close-alert').addEventListener('click', () => {
                        location.reload();
                    });
                }
            });
        }
    }

    let checkExistenceTransaction = (action) => {
        $.ajax({
            url : `${URL}/check/${ID}`,
            method: "GET",
            success : () => {
                if(action == 'edit'){
                    getShipmentByID();
                }else if(action == 'delete'){
                    showConfirm("Are you sure you want to delete this shipment?", "delete");
                }
            },
            error : (response) => {
                showAlert(response.responseText);
            }
        });
    }

    let bindingDataToModalUpsert = (data) => {
        document.querySelector('input[name=name]').value = data.name;
        document.querySelector('input[name=price]').value = data.price;
        document.querySelector('input[name=service]').checked = data.isService;
    };

    let openModalUpsert = (title) => {
        document.querySelector('h4.title').textContent = title;
        document.querySelector('.modal-layer').style.display = 'flex';
        document.querySelector('.popup-dialog').style.display = 'block';
    };

    let showConfirm = (confirm, action) => {
        document.querySelector('.modal-confirm').style.display = 'flex';
        document.querySelector('.popup-confirm').style.display = 'block';
        document.querySelector('#show-confirm').textContent = confirm;
        document.querySelector('#yes-button').addEventListener('click', () => {
            if(action == "stop_service"){
                closeModalConfirm();
                stopService();
            }else if(action == "delete"){
                closeModalConfirm();
                deleteShipment();
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

    //trigger button add
    document.querySelector('#add-button').addEventListener('click', () => {
        ID = 0;
        openModalUpsert('INSERT SHIPMENT');
    });

    //trigger button edit, delete, stop service
    document.querySelector('#data').addEventListener('click', (event) => {
        ID = event.target.getAttribute('id');
        let className = event.target.getAttribute('class');
        if(className == 'edit'){
            checkExistenceTransaction(className);
        }else if(className == 'delete'){
            checkExistenceTransaction(className);
        }else if(className == 'stop_service'){
            showConfirm("Are you sure you want to stop service this shipment?", "stop_service");
        }

    });

    //trigger button submit
    document.querySelector('#upsert').addEventListener('submit', (event) => {
        event.preventDefault();
        let name = document.querySelector('input[name=name]').value;
        let price = document.querySelector('input[name=price]').value;
        let isService = document.querySelector('input[name=service]').checked;
        let data = {
            Id: ID,
            Name: name,
            Price: price,
            IsService: isService
        }
        if(ID == 0){
            insertShipment(data);
        }else{
            updateShipment(data);
        }
    });

    //close modal
    document.querySelector('#cancel-button').addEventListener('click', () => {
        document.querySelector('.modal-layer').style.display = 'none';
        document.querySelector('.popup-dialog').style.display = 'none';
        bindingDataToModalUpsert({ name: null, price: null, isService: false});
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