import * as yup from "yup";

export const addMovieSchema = yup.object({
  title: yup
    .string()
    .required("Title is required")
    .min(3, "Title must containt at least 3 characters")
    .max(50, "Title must containt at most 50 characters"),
    description: yup
    .string()
    .required("Description is required")
    .min(10, "Description must containt at least 10 characters")
    .max(200, "Description must containt at most 200 characters"),
    rating: yup
    .number()
    .typeError('Rating must be a number')
    .min(1, "Rating can't be lower than 1")
    .max(10, "Rating can't be higher than 10"),
    duration: yup
    .number()
    .typeError('Duration must be a number')
    .min(60, "Duration can't be lower than 60")
    .max(200, "Duration can't be higher than 200"),
    country: yup
    .string()
    .required("Country is required"),
    budget: yup
    .number()
    .typeError('Budget must be a number')
    .min(5, "Budget can't be lower than 5")
    .max(500, "Budget can't be higher than 500"),
});
