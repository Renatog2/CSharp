const input = document.querySelector('#inputCPF');

input.addEventListener('keypress', () => {
    let inputlength = input.value.length;

    if (!/[0-9]/.test(event.key)) {
        event.preventDefault();
        return;
    }

    if (inputlength === 3 || inputlength === 7) {
        input.value += ".";
    } else if (inputlength === 11) {
        input.value += "-";
    }
});