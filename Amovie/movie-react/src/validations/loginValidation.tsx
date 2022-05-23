import * as yup from "yup";

export const loginSchema = yup.object({
  email: yup
  .string()
  .required("Email is required")
  .email("Invalid mail"),
  password: yup
    .string()
    .required("Password is required")
    .min(6, "Password must containt at least 6 characters")
    .max(15, "Password must containt at most 15 characters"),
});
