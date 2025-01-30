$(document).ready(function () { loadData() });
var dataTable;
function loadData() {
    dataTable = $('#productTbl').DataTable({
        ajax: { url: '/admin/product/getall', dataSrc: 'data' },
        columns: [
            { data: 'title', title: 'Title',"width":"15%" },
            { data: 'author', title: 'Author', "width": "15%" },
            { data: 'listPrice', title: 'List Price', "width": "15%" },
            { data: 'category.name', title: 'Category', "width": "15%" },
            {
                data: 'id',
                render: function (data) {
                    return `
                        <a href="Product/Upsert/${data}" class="btn btn-group btn-dark" role="group">
					    <i class="bi bi-pencil-square pe-2"></i>  Edit
	 				</a>
                    `;
                }
            },
            {
                data: 'id',
                render: function (data) {
                    return `
                        <a onClick=Delete("product/delete/${data}") class="btn btn-group btn-danger" role="group">
					    <i class="bi bi-pencil-square pe-2"></i>  Delete
	 				</a>
                    `;
                }, "width": "15%" 
            }
        ]
    });
}

function Delete(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: "DELETE",
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })
        }
    });
}