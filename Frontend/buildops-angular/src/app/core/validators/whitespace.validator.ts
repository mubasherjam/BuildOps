import {
  AbstractControl,
  ValidationErrors
} from '@angular/forms';

export function whitespaceValidator(
  control: AbstractControl
): ValidationErrors | null {

  const value = control.value;

  if (
    typeof value === 'string' &&
    value.length > 0 &&
    value.trim().length === 0
  ) {
    return { whitespace: true };
  }

  return null;
}