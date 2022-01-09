$(document).ready(function () {
    var a = new Logins();
})
class Logins {
    constructor() {
        this.initButtons();
    }
    initButtons() {
        $('#btnLogin').click(this.btnLoginOnClick.bind(this));
    }
    btnLoginOnClick() {
        var nick = {};
        nick.Email = $('#Username').val();
        nick.PassWord = $('#Password').val();
        
        $.ajax({
            url: 'https://localhost:44312/getvalueLogin/' + nick.Email + '/' + nick.PassWord,
            contentType: 'application/json',
            dataType: 'json'
        }).done(function (res) {
            alert(res);
            if (res == 'OK!') {
                window.location.replace('./DashBoard.html');
            }
            else {
                alert('Nhập lại!');
            }
        }).fail(function (res) {
            alert(res);
        })
    }

}