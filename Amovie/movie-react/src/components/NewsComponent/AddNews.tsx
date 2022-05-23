import { yupResolver } from "@hookform/resolvers/yup";
import {
  Container,
  Grid,
  TextField,
  Typography,
  Box,
  Button,
} from "@mui/material";
import { useState } from "react";
import { useForm } from "react-hook-form";
import { Redirect } from "react-router-dom";
import { addNewsSchema } from "../../validations/addNewsValidation";
import { AdapterDateFns } from "@mui/x-date-pickers/AdapterDateFns";
import { LocalizationProvider } from "@mui/x-date-pickers/LocalizationProvider";
import { DatePicker } from "@mui/x-date-pickers/DatePicker";
import moment from "moment";
import InputLabel from "@mui/material/InputLabel";
import FormControl from "@mui/material/FormControl";
import NativeSelect from "@mui/material/NativeSelect";
import useFetch from "../../hooks/useFetch";
import { AuthorType, NewsType } from "../../Types/Types";

export default function AddNews() {
  const [redirect, setRedirect] = useState(false);
  const [dateValue, setDateValue] = useState<Date | null>(null);

  const url = `http://localhost:7063/api/Author/allauthors`;
  const { data: authors } = useFetch<AuthorType[]>(url);

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<NewsType>({ resolver: yupResolver(addNewsSchema) });

  const onSubmit = async (values: any) => {
    const url = "http://localhost:7063/addnews";

    const data = new FormData();
    data.append("title", values.title);
    data.append("image", values.image[0]);
    data.append("content", values.content);
    data.append("date", moment(dateValue).format("YYYY-MM-DD"));
    data.append("authorId", values.authorId);
    try {
      await fetch(url, {
        method: "POST",
        body: data,
      });
    } catch (error) {
      console.error("Error:", error);
    }
    setRedirect(true);
  };

  if (redirect) {
    return <Redirect to="/news" />;
  }

  return (
    <Container>
      <FormControl
        encType="multipart/form-data"
        component="form"
        onSubmit={handleSubmit(onSubmit)}
        sx={{ display: "block", mt: 4, mb: 4 }}
      >
        
        <Grid container spacing={2} sx={{ display: "block" }}>
          <Grid item xs={6}>
            <TextField
              fullWidth
              id="title"
              label="News title"
              {...register("title", {})}
            />
            {errors.title && (
              <Typography style={{ color: "#F95252" }}>
                * {errors.title.message}
              </Typography>
            )}
          </Grid>

          <Grid item xs={6}>
            <TextField
              fullWidth
              id="content"
              multiline
              minRows={3}
              label="News content"
              {...register("content", {})}
            />
            {errors.content && (
              <Typography style={{ color: "#F95252" }}>
                * {errors.content.message}
              </Typography>
            )}
          </Grid>

          <Grid item xs={6} md={3}>
            <TextField fullWidth type="file" {...register("image")}></TextField>
            <label htmlFor="choose-file"></label>
            {errors.image && (
              <Typography style={{ color: "#F95252" }}>
                * {errors.image.message}
              </Typography>
            )}
          </Grid>

          <Grid item xs={6} md={3}>
            <LocalizationProvider dateAdapter={AdapterDateFns}>
              <DatePicker
                label="News Date"
                value={dateValue}
                onChange={(newValue) => {
                  setDateValue(newValue);
                }}
                renderInput={(register) => (
                  <TextField fullWidth {...register} name="date" />
                )}
              />
            </LocalizationProvider>
            {errors.date && (
              <Typography style={{ color: "#F95252" }}>
                * {errors.date.message}
              </Typography>
            )}
          </Grid>
          <Grid item xs={6} md={3}>
            <FormControl fullWidth>
              <InputLabel
                variant="standard"
                htmlFor="uncontrolled-native"
              ></InputLabel>
              <NativeSelect defaultValue={30} {...register("authorId")}>
                {authors &&
                  authors?.map((author) => (
                    <option key={author.id} value={author.id}>
                      {author.firstName.concat(" ")}
                      {author.lastName}
                    </option>
                  ))}
              </NativeSelect>
            </FormControl>
            {errors.authorId && (
              <Typography style={{ color: "#F95252" }}>
                * {errors.authorId.message}
              </Typography>
            )}
          </Grid>
          <Button type="submit" variant="contained" sx={{ mt: 3, ml: 2 }}>
            Add news
          </Button>
        </Grid>
      </FormControl>
    </Container>
  );
}
