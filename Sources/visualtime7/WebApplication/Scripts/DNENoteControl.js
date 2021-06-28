function VerifySelectedDateIsGreaterThanToday(selectedDate) {
    var currentDate = new Date();
    if (((selectedDate != null) && (selectedDate > currentDate)) || (selectedDate == null)) {
        return true;
    }
    return false;
}