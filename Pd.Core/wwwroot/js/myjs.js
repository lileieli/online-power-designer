

function Alert(id, text) {
    var content = '<div class="alert alert-warning alert-dismissable"><button type="button" class="close" data-dismiss="alert"aria-hidden="true">&times;</button>' + text + '</div>'

    id.prepend(content)
}