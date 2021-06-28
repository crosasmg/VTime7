var DateAndTime = new function () {

    this.HorasEntreDosFechas = function (date1, date2) {
        var formatted = '00:00';
        start_actual_time = new Date(date1);
        end_actual_time = new Date(date2);

        if (start_actual_time < end_actual_time) {
            var diff = end_actual_time - start_actual_time;
            var diffSeconds = diff / 1000;
            var HH = Math.floor(diffSeconds / 3600);
            var MM = Math.floor(diffSeconds % 3600) / 60;

            formatted = (HH < 10 ? "0" + HH : HH) + ":" + (MM < 10 ? "0" + MM : MM);
        }
        return formatted;
    };

    this.HorasEntreDosFechas2 = function (date1, date2) {
        var formatted = moment('0001-01-01T00:00:00');
        start_actual_time = new Date(date1);
        end_actual_time = new Date(date2);

        if (start_actual_time < end_actual_time) {
            formatted = moment({ milliseconds: end_actual_time - start_actual_time });
        }
        return formatted.toDate();
    };

};