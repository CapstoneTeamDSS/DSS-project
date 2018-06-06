function LandingApp(name, description, htmlClass, reference) {
    this.value = $('<li/>', {
        'class': htmlClass,
        html: [$('<a/>', {
            'class': 'app-name',
            'href': reference,
            'html': name
        }), $('<p/>', {
            'html': description
        })]
    });
}
LandingApp.prototype.color = function (c) {
    this.value.attr('class', c + ' icon-palm');
}
LandingApp.prototype.values = function () {
    return this.value;
}
var color = ['brown','purple'];
$(document).ready(function () {
    for (var i in window['LandingApps']) {
        var app = window['LandingApps'][i];
        app.color(color[i]);
        $('[data-role=landing-app-container]').append(app.value);
    }
})