import { yupResolver } from "@hookform/resolvers/yup";
import {
  Container,
  Box,
  Grid,
  TextField,
  Typography,
  FormControl,
  InputLabel,
  Button,
  Select,
  MenuItem,
  OutlinedInput,
  SelectChangeEvent,
} from "@mui/material";
import { LocalizationProvider, DatePicker } from "@mui/x-date-pickers";
import { AdapterDateFns } from "@mui/x-date-pickers/AdapterDateFns";
import moment from "moment";
import { useState } from "react";
import { useForm } from "react-hook-form";
import { Redirect } from "react-router-dom";
import useFetch from "../../hooks/useFetch";
import { MovieType } from "../../Types/Types";
import { addMovieSchema } from "../../validations/addMovieValidation";

export default function AddMovie() {
  const [redirect, setRedirect] = useState(false);
  const [dateValue, setDateValue] = useState<Date | null>(null);
  const [actorName, setActorName] = useState<string[]>([]);
  const [genreName, setGenreName] = useState<string[]>([]);

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<MovieType>({ resolver: yupResolver(addMovieSchema) });

  const actorUrl = `http://localhost:7063/api/Actor/allactors`;
  const { data: actors } = useFetch<any[]>(actorUrl);

  const genreUrl = `http://localhost:7063/api/Genre/allgenres`;
  const { data: genres } = useFetch<any[]>(genreUrl);

  const onSubmit = async (values: any) => {
    console.log("works");
    const url = "http://localhost:7063/create";
    const data = {
      title: values.title,
      image: values.image[0].name,
      description: values.description,
      release: moment(dateValue).format("YYYY-MM-DD"),
      rating: values.rating,
      duration: values.duration,
      country: values.country,
      budget: values.budget,
      genreId: genreName,
      actorId: actorName,
    };
    try {
      await fetch(url, {
        method: "POST",
        body: JSON.stringify(data),
        headers: {
          "Content-Type": "application/json",
        },
      });
      console.log("data ", data);
    } catch (error) {
      console.error("Error:", error);
    }
    setRedirect(true);
  };

  if (redirect) {
    return <Redirect to="/movies" />;
  }

  const handleGenresChange = (event: SelectChangeEvent<typeof actorName>) => {
    const {
      target: { value },
    } = event;
    setActorName(
      typeof value === "string" ? value.split(",") : value
    );
  };

  const handleActorsChange = (event: SelectChangeEvent<typeof genreName>) => {
    const {
      target: { value },
    } = event;
    setGenreName(typeof value === "string" ? value.split(",") : value);
  };

  return (
    <Container>
      <Box
        component="form"
        onSubmit={handleSubmit(onSubmit)}
        sx={{ display: "block", mt: 4, mb: 4 }}
      >
        <Typography style={{ fontSize: 32, fontWeight: 'bold', marginLeft: 5, marginBottom: 10}}>Add Movie</Typography>
        <Grid container spacing={2} sx={{ display: "flex" }} mx={{display: "flex"}}>
          <Grid item xs={5}>
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

          <Grid item xs={5}>
            <TextField fullWidth type="file" {...register("image")}></TextField>
            <label htmlFor="choose-file"></label>
            {errors.image && (
              <Typography style={{ color: "#F95252" }}>
                * {errors.image.message}
              </Typography>
            )}
          </Grid>

          <Grid item xs={5}>
            <TextField
              fullWidth
              id="description"
              multiline
              minRows={3}
              label="News content"
              {...register("description", {})}
            />
            {errors.description && (
              <Typography style={{ color: "#F95252" }}>
                * {errors.description.message}
              </Typography>
            )}
          </Grid>

          <Grid item xs={5}>
            <LocalizationProvider dateAdapter={AdapterDateFns}>
              <DatePicker
                label="Movie Date"
                value={dateValue}
                onChange={(newValue) => {
                  setDateValue(newValue);
                }}
                renderInput={(register) => (
                  <TextField fullWidth {...register} name="release" />
                )}
              />
            </LocalizationProvider>
            {errors.release && (
              <Typography style={{ color: "#F95252" }}>
                * {errors.release.message}
              </Typography>
            )}
          </Grid>

          <Grid item xs={5}>
            <TextField
              fullWidth
              id="rating"
              label="Rating"
              {...register("rating")}
            />
            {errors.rating && (
              <Typography style={{ color: "#F95252" }}>
                * {errors.rating.message}
              </Typography>
            )}
          </Grid>

          <Grid item xs={5}>
            <TextField
              fullWidth
              id="duration"
              label="Duration"
              {...register("duration")}
            />
            {errors.duration && (
              <Typography style={{ color: "#F95252" }}>
                * {errors.duration.message}
              </Typography>
            )}
          </Grid>

          <Grid item xs={5}>
            <TextField
              fullWidth
              id="country"
              label="Country"
              {...register("country")}
            />
            {errors.country && (
              <Typography style={{ color: "#F95252" }}>
                * {errors.country.message}
              </Typography>
            )}
          </Grid>

          <Grid item xs={5}>
            <TextField
              fullWidth
              id="budget"
              label="Budget"
              {...register("budget")}
            />
            {errors.budget && (
              <Typography style={{ color: "#F95252" }}>
                * {errors.budget.message}
              </Typography>
            )}
          </Grid>
        </Grid>

        <Grid sx={{display: 'flex', flexDirection: 'row'}}>
          <FormControl sx={{  mt: 2, width: 300 }}>
            <InputLabel id="demo-multiple-genres-label">Genres</InputLabel>
            <Select
              labelId="demo-multiple-genres-label"
              id="demo-multiple-genres"
              multiple
              value={actorName}
              onChange={handleGenresChange}
              input={<OutlinedInput label="Actors" />}
            >
              {actors?.map((actor) => (
                <MenuItem key={actor?.id} value={actor?.id}>
                  {actor.firstName.concat(" ")}
                  {actor.lastName}
                </MenuItem>
              ))}
            </Select>
          </FormControl>

          <FormControl sx={{ mt: 2, width: 300 }}>
            <InputLabel id="demo-multiple-actors-label">Actors</InputLabel>
            <Select
              labelId="demo-multiple-actors-label"
              id="demo-multiple-actors"
              multiple
              value={genreName}
              onChange={handleActorsChange}
              input={<OutlinedInput label="Genres" />}
            >
              {genres?.map((genre) => (
                <MenuItem key={genre?.id} value={genre?.id}>
                  {genre?.name}
                </MenuItem>
              ))}
            </Select>
          </FormControl>
        </Grid>
        <Button
          type="submit"
          variant="contained"
          sx={{ mt: 3,  width: "120px" }}
        >
          Add movie
        </Button>
      </Box>
    </Container>
  );
}
