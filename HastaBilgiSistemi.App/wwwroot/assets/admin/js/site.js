function convertFirstLetterToUpperCase(text) {
    return text.charAt(0).toUpperCase()+text.slice(1);
}
function covertToShortDate(dateString) {
    const options = { year: 'numeric', month: 'numeric', day: 'numeric', hour: 'numeric', minute: 'numeric' };
    const shortDate = new Date(dateString).toLocaleDateString(undefined, options);
    return shortDate;
}