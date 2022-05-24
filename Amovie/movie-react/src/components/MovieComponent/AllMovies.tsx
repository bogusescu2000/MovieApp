import {
  Box,
  Button,
  CircularProgress,
  FormControl,
  InputLabel,
  MenuItem,
  Pagination,
  Select,
  TextField,
} from "@mui/material";
import InputAdornment from "@mui/material/InputAdornment";
import moment from "moment";
import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import "../../styles/movies.scss";
import { MoviesPage, MovieType } from "../../Types/Types";
import SearchIcon from "@mui/icons-material/Search";

export default function AllMovies() {
  const [page, setPage] = useState(1);
  const [data, setData] = useState<MoviesPage>();
  const [movies, setMovies] = useState<MovieType[]>();
  const [filterValue, setFilterValue] = useState("");
  const [selectValue, setSelectValue] = useState("");

  const url = `http://localhost:7063/pagedmovies?page=${page}&pageSize=12&sort=${selectValue}&title=${filterValue}`;
  const userRole = localStorage.getItem("role");

  useEffect(() => {
    const fetchPost = async () => {
      const response = await fetch(url);
      const data = await response.json();
      setMovies(data.movies);
      setData(data);
    };
    fetchPost();
  }, [url]);

  const deleteMovie = async (id: number) => {
    const url = `http://localhost:7063/api/movies/${id}`;
    await fetch(url, {
      method: "DELETE",
    })
      .then(async (response) => {
        if (response.ok) {
          window.location.reload();
        }
      })
      .catch((error) => {
        console.warn("There was an error!", error);
      });
  };

  return (
    <div className="container">
      <div className="movies-title">
        <div className="title-block">
          <p className="title">Movies</p>
          <div className="blue-line">
            <div> </div>
          </div>
        </div>
      </div>
      <Pagination
        size="large"
        count={data?.pages}
        page={page}
        siblingCount={0}
        onChange={(_, page) => setPage(page)}
      />

      <Box sx={{ display: "flex", justifyContent: "space-between" }}>
        <FormControl>
          <InputLabel id="demo-simple-select-label">Sorting </InputLabel>
          <Select
            fullWidth
            labelId="demo-simple-select-label"
            label="Sorting"
            id="demo-simple-select"
            value={selectValue}
            onChange={(e: any) => setSelectValue(e.target.value)}
          >
            <MenuItem value={"asc"}>Ascending rating</MenuItem>
            <MenuItem value={"desc"}>Descending rating</MenuItem>
            <MenuItem value={"ascDate"}>Ascending date</MenuItem>
            <MenuItem value={"descDate"}>Descending date</MenuItem>
          </Select>
        </FormControl>
        <TextField
          label="Filter movies"
          InputProps={{
            endAdornment: <SearchIcon />,
          }}
          sx={{ mb: 3 }}
          onChange={(e: any) => setFilterValue(e.target.value)}
        />
      </Box>
      {userRole === "admin" ? (
        <Box sx={{ mb: 3, mt: 0, p: 0 }} className="button container">
          <Link to="/addmovie">
            <button>
              <p>Add movie</p>
            </button>
          </Link>
        </Box>
      ) : (
        ""
      )}
      <div className="movies-container">
        {movies &&
          movies?.map((movie) => (
            <div className="movie" key={movie.id}>
              <div className="image">
                <Link to={`/movies/${movie.id}`}>
                  <img
                    src={`http://localhost:7063/images/${movie.image}`}
                    alt={movie.title}
                  />
                </Link>
              </div>
              <div className="content">
                <div className="title-rating">
                  <h2>{movie.title}</h2>
                </div>
                <div className="date">
                  <div>
                    <span className="dot"></span>
                    <p>{moment(movie.release).format("YYYY")}</p>
                  </div>
                  <div>
                    <span className="dot"></span>
                    <p>{movie.duration} min</p>
                  </div>
                  <div>
                    <span className="dot"></span>
                    <p>
                      {" "}
                      IMDb <span>{movie.rating}</span>
                    </p>
                  </div>
                </div>
                {userRole === "admin" ? (
                  <Box
                    sx={{ justifyContent: "center", display: "flex", mt: 1 }}
                  >
                    <Button
                      onClick={() => deleteMovie(movie.id)}
                      variant="contained"
                      color="error"
                    >
                      Delete
                    </Button>
                  </Box>
                ) : (
                  ""
                )}
              </div>
            </div>
          ))}
      </div>
    </div>
  );
}
