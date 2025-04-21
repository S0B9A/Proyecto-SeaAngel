const nameInput = document.querySelector('input[name="Nombre"]');
const continueButtonStep1 = document.querySelector('#step-1 .btn-primary');

function validateName() {
    const nameValue = nameInput.value.trim();
    const nameRegex = /^[A-Za-zÀ-ÖØ-öø-ÿ\s]+$/; // Letters and spaces only, including accented characters

    if (!nameRegex.test(nameValue) || nameValue === '') {
        nameInput.classList.add('is-invalid');
        nameInput.classList.remove('is-valid');
        continueButtonStep1.disabled = true;
        return false;
    } else {
        nameInput.classList.remove('is-invalid');
        nameInput.classList.add('is-valid');
        continueButtonStep1.disabled = false;
        return true;
    }
}
nameInput.addEventListener('input', validateName);

const emailInput = document.querySelector('input[name="CorreoElectronico"]');
const continueButtonStep2 = document.querySelector('#step-2 .btn-primary');
const dobInput = document.querySelector('input[name="FechaNacimiento"]');

function validateEmail() {

    const emailValue = emailInput.value.trim();
    const emailRegex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;

    if (!emailRegex.test(emailValue)) {
        emailInput.classList.add('is-invalid');
        emailInput.classList.remove('is-valid');
        return false;
    } else {
        emailInput.classList.remove('is-invalid');
        emailInput.classList.add('is-valid');
        return true;
    }
}
function validateDob() {
    const dobValue = dobInput.value;

    if (dobValue === '') {
        return false;
    }

    const selectedYear = new Date(dobValue).getFullYear();
    return selectedYear < 2011;
}

function updateStep2Button() {
    if (!(validateEmail() && validateDob())) {
        continueButtonStep2.disabled = true;
    } else {
        continueButtonStep2.disabled = false;
    }
}

emailInput.addEventListener('input', updateStep2Button);
dobInput.addEventListener('input', updateStep2Button);
dobInput.addEventListener('change', updateStep2Button);

// 3. Phone validation - only numbers
const phoneInput = document.querySelector('input[name="Telefono"]');
const countrySelect = document.querySelector('select[name="Pais"]');
const continueButtonStep3 = document.querySelector('#step-3 .btn-primary');

function validatePhone() {
    const phoneValue = phoneInput.value.trim();
    const phoneRegex = /^\+?[0-9\s()-]+$/;
    const digitsOnly = phoneValue.replace(/\D/g, ''); // quita todo menos dígitos
    const digitCount = digitsOnly.length;

    if (!phoneRegex.test(phoneValue) || phoneValue === '' || digitCount < 8) {
        phoneInput.classList.add('is-invalid');
        phoneInput.classList.remove('is-valid');
        return false;
    } else {
        phoneInput.classList.remove('is-invalid');
        phoneInput.classList.add('is-valid');
        return true;
    }
}

function validateCountry() {
    return countrySelect.value !== '' && countrySelect.value !== null;
}
function updateStep3Button() {
    if (!(validatePhone() && validateCountry())) {
        continueButtonStep3.disabled = true;
    } else {
        continueButtonStep3.disabled = false;
    }
}

phoneInput.addEventListener('input', updateStep3Button);
countrySelect.addEventListener('change', updateStep3Button);

// Initialize validation on page load
window.addEventListener('DOMContentLoaded', function () {
    // Set initial button states
    validateName();
    continueButtonStep2.disabled = true;
    continueButtonStep3.disabled = true;
    // Add validation before allowing step transitions
    const originalNextStep = nextStep;
    nextStep = function () {
        let canProceed = false;

        switch (currentStep) {
            case 1:
                canProceed = validateName();
                break;
            case 2:
                canProceed = validateEmail() && validateDob();
                break;
            case 3:
                canProceed = validatePhone() && validateCountry();
                break;
            default:
                canProceed = true;
        }

        if (canProceed) {
            originalNextStep();
        }
    };
});