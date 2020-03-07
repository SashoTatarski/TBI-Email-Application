
$(document).ready(function () {
    $('.preview-btn').click(function (event) {
        const id = $(event.target).attr('data-model-id');
        $('#preview-all-email-modal-' + id).modal('show');

        $('#modal-body-' + id).html(`
        <div class="d-flex justify-content-center">
            <div class="spinner-border mt-3 mb-3" style="color: #EC7424;"></div>
        </div>
`);

        $.get('/email/emailbody?id=' + id, function (data) {
            $('#modal-body-' + id).html(data);
        });
    });

    $('#show-only-mine-checkbox').change(function (ev) {
        const isChecked = ev.target.checked;
        const currentUser = $('#show-only-mine-checkbox').data('for-user');

        $('tbody tr').each((i, tr) => {
            if ($(tr).data('user') !== currentUser && isChecked) {
                $(tr).hide();
            } else {
                $(tr).show();
            }
        });
    });

    $('#search-user').on('input', function (ev) {

        $('tbody tr .operator').each((i, tr) => {
            if ($(tr).text().includes(ev.target.value)) {
                $(tr).parent().show();
            } else {
                $(tr).parent().hide();
            }
        })
    });
});