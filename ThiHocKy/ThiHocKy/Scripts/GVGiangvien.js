$(document).ready(function () {
    var init = new GVGiangviens();
})
class GVGiangviens {
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
        $('#MaGV').blur(this.suggestNameGV);
    }

    btnShowfunction() {
        $(".tbody-body").empty();
        $.ajax({
            url: 'https://localhost:44312/api/GVGiangVien',
            method: 'GET',
            contentType: 'application/json'
        }).done(function (classroom) {
            $.each(classroom, function (index, item) {
                var add_item = $(`<tr>
                <td>` + item.MaLT + `</td>
                <td>` + item.MaGV + `</td>
                <td>` + item.TenGV + `</td>                
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
    }
    hideData() {
        $('.detail-add').hide();
        $('.back-add-detail').hide();
    }

    btnSavefunction() {
        var temp = this;
        var method = 'PUT';
        var giangvien = {};
        giangvien.MaLT = $('#MaLT').val();
        giangvien.MaGV = $('#MaGV').val();
        giangvien.TenGV = $('#TenGV').val();

        if (!giangvien.MaLT || !giangvien.MaGV) {
            alert('Bạn Nhập thiếu, Không thể thêm data');
        }
        else {
            if (this.Mode == "Add") {
                method = 'POST';
            }
            $.ajax({
                url: 'https://localhost:44312/api/GVGiangVien/P',
                method: method,
                data: JSON.stringify(giangvien),
                contentType: 'application/json',
                dataType: 'json'
            }).done(function (response) {
                alert(response);
                temp.hideData();
                temp.btnShowfunction();
                temp.Mode = null;
            }).fail(function (response) {

            })
        }
    }
    btnEditfunction() {
        var selectedTemp = $('table tbody .row-selected');
        if (selectedTemp.length <= 0) {
            alert('Bạn chưa chọn thành phần để sửa');
        }
        else {
            this.showData();
            var IDTemp = $(selectedTemp).children()[1].textContent;
            $.ajax({
                url: 'https://localhost:44312/getValueGiangVien/' + parseInt(IDTemp),
                method: 'GET',
            }).done(function (res) {
                if (!res) {
                    alert('Không có dữ liệu này!');
                }
                else {
                    $('#MaLT').val(res["MaLT"]);
                    $('#MaGV').val(res["MaGV"]);
                    $('#TenGV').val(res["TenGV"]);
                }
            }).fail(function (res) {
                alert('b');
            })
        }
    }
    btnDelfunction() {
        var temp = this;
        var selectedTemp = $('table tbody .row-selected');
        var IDTemp2 = $(selectedTemp).children()[1].textContent;
        $.ajax({
            url: 'https://localhost:44312/api/GVGiangVien/Delete/' + parseInt(IDTemp2),
            method: 'DELETE',
        }).done(function (res) {
            temp.btnShowfunction();
        }).fail(function (res) {
            alert('Fail!');
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
    suggestNameGV() {
        var IDTemp = $('#MaGV').val();
        $.ajax({
            url: 'https://localhost:44312/getValueGiangVienOnly/' + parseInt(IDTemp),
            method: 'GET',
        }).done(function (res) {
            if (!res) {
                alert('Không có dữ liệu này!');
            }
            else {
                $('#TenGV').val(res["TenGV"]);
            }
        }).fail(function (res) {
            alert('b');
        })
    }
}