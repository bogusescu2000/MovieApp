import * as yup from "yup";

export const addReviewSchema = yup.object({
  content: yup
    .string()
    .required("Content is required")
    .min(10, "Content must containt at least 10 characters")
    .max(200, "Content must containt at most 200 characters"),
});
