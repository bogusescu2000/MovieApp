import * as yup from "yup";

export const addNewsSchema = yup.object({
  title: yup
    .string()
    .required("Title is required")
    .min(3, "Title must containt at least 3 characters")
    .max(50, "Title must containt at most 50 characters"),
  content: yup
    .string()
    .required("Content is required")
    .min(10, "Content must containt at least 10 characters")
    .max(1000, "Content must containt at most 1000 characters"),
  date: yup
  .string()
  .default("2022-01-01")
});
