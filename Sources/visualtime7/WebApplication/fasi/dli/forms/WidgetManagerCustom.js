function WidgetIconTemplate(icon) {
    if (!icon.id) {
        return icon.text;
    }
    var $icon = $('<span></span>').append($('<i class="' + icon.text + '"></i>').css({
        'color': icon.text
    })).append(' '+ icon.text);
    
    return $icon;
};

function WidgetIconFormatter(value, row, index) {
    var result = '';
    if (value === 0 || value === "") {
        result = '';
    } else {
        result = $("#Icon>option[value='" + value + "']").text();

        result = $('<span></span>').append($('<i class="' + result + '"></i>').css({
            'color': result
        })).html();

    }
    return result;
};