var card = {
    init: function () {
        card.regEvents();
    },
    regEvents: function(){
        $('#btnContinue').off('click').on('click', function () {
            window.location.href = "/";
        });

        $('#btnUpdate').off('click').on('click', function () {
            var listTour = $('.txtQuantity');
            var cardList = [];
            $.each(listTour, function (i, item) {
                cardList.push({
                    SoNguoi: $(item).val(),
                    Tour: {
                        Id: $(item).data('id')
                    }
                });

                $.ajax({
                    url: '/Card/Update',
                    data: { cardModel: JSON.stringify(cardList) },
                    dataType: 'json',
                    type: 'POST',
                    success: function (res) {
                        if (res.status == true)
                        {
                            window.location.href = "/Card";
                        }
                    }
                })
            });
        });

        $('.btn-delete').off('click').on('click', function (e) {
            e.preventDefault();
            $.ajax({
                url: '/Card/Delete',
                data: { id: $(this).data('id') },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/Card";
                    }
                }
            })
        });

        $('#btnOrderTour').off('click').on('click', function () {
            window.location.href = "/OrderTour";
        });
        
        
        $(function () {
            var today = new Date();
            var dd = today.getDate();
            var mm = today.getMonth() + 1; //January is 0!
            var yyyy = today.getFullYear();

            if (dd < 10) {
                dd = '0' + dd
            }

            if (mm < 10) {
                mm = '0' + mm
            }

            today = mm + '/' + dd + '/' + yyyy;
            $('input[name="datetimepickerOrder"]').val(today);

            //var date_input = $('input[name="datetimepickerOrder"]'); //our date input has the name "date"
            //var container = $('.bootstrap-iso form').length > 0 ? $('.bootstrap-iso form').parent() : 'body';
            //var options = {
            //    format: 'mm/dd/yyyy',
            //    container: container,
            //    todayHighlight: true,
            //    autoclose: true,
            //};

            //date_input.datepicker(options);

        });

        function CheckDateInput() {
            var today = new Date();
            var dd = today.getDate();
            var mm = today.getMonth() + 1; //January is 0!
            var yyyy = today.getFullYear();

            if (dd < 10) {
                dd = '0' + dd
            }

            if (mm < 10) {
                mm = '0' + mm
            }

            today = mm + '/' + dd + '/' + yyyy;

            var date_input = $('input[name="datetimepickerOrder"]').val();
            alert(date_input);
            if (date_input < today) {
                return false;
            }
        }
    }
}
card.init();