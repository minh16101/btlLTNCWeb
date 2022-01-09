$(document).ready(function () {
    var init = new GVLopHocs();
})
class GVLopHocs {
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
    }

    btnShowfunction() {
        $(".tbody-body").empty();
        $.ajax({
            url: 'https://localhost:44312/api/GVLopHoc',
            method: 'GET',
            contentType: 'application/json'
        }).done(function (classroom) {
            $.each(classroom, function (index, item) {
                var add_item = $(`<tr>
                <td>` + item.MaLH + `</td>
                <td>` + item.TenLH + `</td>
                <td>` + item.MaGV + `</td>
                <td>` + item.MaHP + `</td>
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
        $('#MaLH').focus();
    }
    btnCanfunction() {
        this.hideData();
        $('#MaLH').removeAttr('disabled', 'disabled');//c2
        $('#MaLH').removeClass('disable-value')
    }
    hideData() {
        $('.detail-add').hide();
        $('.back-add-detail').hide();
    }
    updateTenLopHoc() {

    }
    btnSavefunction() {
        var temp = this;
        var method = 'PUT';
        var lophoc = {};
        lophoc.MaLH = $('#MaLH').val();
        lophoc.TenLH = $('#TenLH').val();
        lophoc.MaGV = $('#MaGV').val();
        lophoc.MaHP = $('#MaHP').val();
        if (!lophoc.MaLH || !lophoc.TenLH || !lophoc.MaGV) {
            alert('Bạn Nhập thiếu, Không thể thêm data');
        }
        else {
            if (this.Mode == "Add") {
                method = 'POST';
            }
            $.ajax({
                url: 'https://localhost:44312/api/GVLopHoc/P',
                method: method,
                data: JSON.stringify(lophoc),
                contentType: 'application/json',
                dataType: 'json'
            }).done(function (response) {
                alert(response);
                temp.hideData();
                temp.btnShowfunction();
                temp.Mode = null;
                $('#MaLH').removeAttr('disabled', 'disabled');//c2
                $('#MaLH').removeClass('disable-value')
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
                url: 'https://localhost:44312/getValue/' + parseInt(IDTemp),
                method: 'GET',
            }).done(function (res) {
                if (!res) {
                    alert('Không có dữ liệu này!');
                }
                else {
                    $('#MaLH').val(res["MaLH"]);
                    $('#TenLH').val(res["TenLH"]);
                    $('#MaGV').val(res["MaGV"]);
                    $('#MaHP').val(res["MaHP"]);
                    $('#MaLH').attr('disabled', 'disabled');//c2
                    $('#MaLH').addClass('disable-value')
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
            url: 'https://localhost:44312/api/GVLopHoc/Delete/' + parseInt(IDTemp),
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
}