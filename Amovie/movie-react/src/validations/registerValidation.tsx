import * as yup from "yup";

export const registerSchema = yup.object({
  name: yup
    .string()
    .required("Name is required")
    .min(3, "Name must containt at least 3 characters")
    .max(20, "Name must contain at most 20 characters"),
  email: yup
  .string()
  .required("Email is required")
  .email("Invalid mail"),
  password: yup
    .string()
    .required("Password is required")
    .min(6, "Password must containt at least 6 characters")
    .max(15, "Password must containt at most 15 characters"),
  confirmPassword: yup
    .string()
    .required("Confirm password is required")
    .oneOf([yup.ref("password"), null], "Password must match"),
});
