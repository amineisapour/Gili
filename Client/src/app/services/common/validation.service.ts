export class ValidationService {
    public static getValidatorErrorMessage(validatorName: string, validatorValue?: any) {
        let config: any = {
            required: 'Required',
            invalidCreditCard: 'Is invalid credit card number',
            invalidEmailAddress: 'Invalid email address',
            invalidNationalId: 'National ID must be a number and min length and max length must be 10',
            invalidPassword:
                'Invalid password. Password must be at least 8 characters long, and contain a number.',
            minlength: `Minimum length ${validatorValue.requiredLength}`
        };

        return config[validatorName];
    }

    static creditCardValidator(control: any) {
        // Visa, MasterCard, American Express, Diners Club, Discover, JCB
        if (
            control.value.match(
                /^(?:4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]{14}|6(?:011|5[0-9][0-9])[0-9]{12}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|(?:2131|1800|35\d{3})\d{11})$/
            )
        ) {
            return null;
        } else {
            return { invalidCreditCard: true };
        }
    }

    static emailValidator(control: any) {
        // RFC 2822 compliant regex
        if (
            control.value.match(
                /[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?/
            )
        ) {
            return null;
        } else {
            return { invalidEmailAddress: true };
        }
    }

    static nationalIdValidator(control: any) {
        // /^[0-9]{10}$/g
        if (control.value.match(/^(?:\d{10}|)$/g)) {
            return null;
        } else {
            return { invalidNationalId: true };
        }
    }

    static passwordValidator(control: any) {
        // {6,100}           - Assert password is between 6 and 100 characters
        // (?=.*[0-9])       - Assert a string has at least one number
        //if (control.value.match(/^(?=.*[0-9])[a-zA-Z0-9!@#$%^&*]{6,100}$/)) {


        /*
        ^	                The password string will start this way
        (?=.*[a-z])	        The string must contain at least 1 lowercase alphabetical character
        (?=.*[A-Z])	        The string must contain at least 1 uppercase alphabetical character
        (?=.*[0-9])	        The string must contain at least 1 numeric character
        (?=.*[!@#$%^&*])    The string must contain at least one special character, but we are escaping reserved RegEx characters to avoid conflict
        (?=.{8,})	        The string must be eight characters or longer
        */
        let strongRegex = new RegExp("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{8,})");
        if (control.value.match(strongRegex)) {
            return null;
        } else {
            return { invalidPassword: true };
        }
    }
}