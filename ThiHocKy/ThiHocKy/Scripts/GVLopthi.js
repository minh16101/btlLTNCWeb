$(document).ready(function () {
    var init = new GVLopThis();
})
class GVLopThis {
    constructor() {
        this.Mode = null;
        this.initButtons();
    }
    initButtons() {
        $('#btnShow').click(this.btnShowfunction.bind(this));
        $('#btnAdd').click(this.btnAddfunction.bind(this));
        $('#btnSave').click(this.btnSavefunction.bind(this));
        $('#btnEdit').click(this.btnEditfunction.bind(this));
        $('#btnDel').click(this.btnDelfunction.bind(this));
        $('#btnCancle').click(this.btnCanfunction.bind(this));
        $('table tbody').on("click", "tr", this.setSelectedRow);
        $('.required').blur(this.checkRequired);
        $('#TenLT').blur(this.suggestNameSubject);
    }
    updateLH() {

    }
    btnShowfunction() {
        $(".tbody-body").empty();
        $.ajax({
            url: 'https://localhost:44312/api/GVLopThi',
            method: 'GET',
            contentType: 'application/json'
        }).done(function (classroom) {
            $.each(classroom, function (index, item) {
                var add_item = $(`<tr>
                <td>` + item.MaLT + `</td>
                <td>` + item.MaLH + `</td>
                <td>` + item.TenLT + `</td>
                <td>` + item.SoGV + `</td>
                <td>` + item.MaGV1 + `</td>
                <td>` + item.MaGV2 + `</td>
                <td>` + item.Time + `</td>
                <td>` + item.Location + `</td>
                <td>` + item.HinhThuc + `</td>
                    </tr>`);
                $(".tbody-body").append(add_item);
            })

        }).fail(function (classroom) {
            alert("Khong on roi!");
        })
    }
    btnAddfunction() {
        this.showData();
        this.Mode = "Add";
    }
    showData() {
        $('.back-add-detail').show();
        $('.detail-add').show();
        $('#MaLT').focus();
    }
    btnCanfunction() {
        this.hideData();
        $('#MaLT').removeAttr('disabled', 'disabled');//c2
        $('#MaLT').removeClass('disable-value');
    }
    hideData() {
        $('.detail-add').hide();
        $('.back-add-detail').hide();
    }

    btnSavefunction() {
        var temp = this;
        var method = 'PUT';
        var lopthi = {};
        lopthi.MaLT = $('#MaLT').val();
        lopthi.MaLH = $('#MaLH').val();
        lopthi.TenLT = $('#TenLT').val();
        lopthi.SoGV = $('#SoLuong').val();
        lopthi.MaGV1 = $('#MaGV1').val();
        lopthi.MaGV2 = $('#MaGV2').val();
        lopthi.Time = $('#Time').val();
        lopthi.Location = $('#Location').val();
        lopthi.HinhThuc = $('#Per').val();
        if (!lopthi.MaLT || !lopthi.MaLH || !lopthi.SoGV || !lopthi.MaGV1 || !lopthi.MaGV2) {
            alert('Bạn Nhập thiếu, Không thể thêm data');
        }
        else {
            if (this.Mode == "Add") {
                method = 'POST';
            }
            $.ajax({
                url: 'https://localhost:44312/api/GVLopThi/P',
                method: method,
                data: JSON.stringify(lopthi),
                contentType: 'application/json',
                dataType: 'json'
            }).done(function (response) {
                alert(response);
                temp.hideData();
                temp.btnShowfunction();
                temp.Mode = null;       
                $('#MaLT').removeAttr('disabled', 'disabled');//c2
                $('#MaLT').removeClass('disable-value')
            }).fail(function (response) {

            })
        }
    }
    btnEditfunction() {
        this.Mode = "Edit";
        var selectedTemp = $('table tbody .row-selected');
        if (selectedTemp.length <= 0) {
            alert('Bạn chưa chọn thành phần để sửa');
        }
        else {
            this.showData();
            var IDTemp = $(selectedTemp).children()[0].textContent;
            $.ajax({
                url: 'https://localhost:44312/getValueLopThi/' + parseInt(IDTemp),
                method: 'GET',
            }).done(function (res) {
                if (!res) {
                    alert('Không có dữ liệu này!');
                }
                else {
                    $('#MaLT').val(res["MaLT"]);
                    $('#MaLH').val(res["MaLH"]);
                    $('#TenLT').val(res["TenLT"]);
                    $('#SoLuong').val(res["SoGV"]);
                    $('#MaGV1').val(res["MaGV1"]);
                    $('#MaGV2').val(res["MaGV2"]);
                    $('#Time').val(res["Time"]);
                    $('#Location').val(res["Location"]);
                    $('#Per').val(res["HinhThuc"]);
                    $('#MaLT').attr('disabled', 'disabled');
                    $('#MaLT').addClass('disable-value');
                }
            }).fail(function (res) {
                alert('b');
            })
        }
    }
    btnDelfunction() {
        var temp = this;
        var selectedTemp = $('table tbody .row-selected');
        var IDTemp = $(selectedTemp).children()[0].textContent;
        $.ajax({
            url: 'https://localhost:44312/api/GVLopThi/Delete/' + parseInt(IDTemp),
            method: 'DELETE',
        }).done(function (res) {
            temp.btnShowfunction();
        }).fail(function (res) {
            alert('b');
        })
    }
    setSelectedRow() {
        $(this).addClass('row-selected').siblings().removeClass('row-selected');
    }
    checkRequired() {
        var value = this.value;
        if (!value) {
            $(this).addClass('required-error');
            $(this).attr("title", "Bạn phải nhập thông tin này!");
        }
        else {
            $(this).removeClass('required-error');
            $(this).removeAttr("title", "Bạn phải nhập thông tin này!");
        }
    }
    suggestNameSubject() {
        var IDTemp = $('#MaLH').val();
        $.ajax({
            url: 'https://localhost:44312/getValue/' + parseInt(IDTemp),
            method: 'GET',
        }).done(function (res) {
            if (!res) {
                alert('Không có dữ liệu này!');
            }
            else {
                $('#TenLT').val(res["TenLH"]);
            }
        }).fail(function (res) {
            alert('b');
        })
    }
}