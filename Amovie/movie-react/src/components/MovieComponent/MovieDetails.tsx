import "../../styles/moviedetails.scss";
import moment from "moment";
import useFetch from "../../hooks/useFetch";
import { useState } from "react";
import { AddReview } from "./AddReview";
import { Button, Typography } from "@mui/material";
import { Link } from "react-router-dom";
import { SingleMovie } from "../../Types/Types";

export default function MovieDetails() {
  const [open, setOpen] = useState(false);
  const url =
    "http://localhost:7063/api/movies/" +
    window.location.pathname.substring(
      window.location.pathname.lastIndexOf("/") + 1
    );
  const { data: movie, error, loading, refetch } = useFetch<SingleMovie>(url);

  let reviewContent;
  const userRole = localStorage.getItem("role");

  if (userRole === "user" || userRole === "admin") {
    reviewContent = <AddReview open={open} setOpen={setOpen} />;
  } else {
    reviewContent = (
      <Typography sx={{ mt: 2 }} style={{ color: "#F95252" }}>
        * You need to<Link to="/signin"> Sign in</Link> first!
      </Typography>
    );
  }

  const deleteReview = async (id: number) => {
    const url = `http://localhost:7063/api/deletereview/${id}`;
    await fetch(url, {
      method: "DELETE",
    })
      .then(async (response) => {
        if (response.ok) {
          await refetch();
        }
      })
      .catch((error) => {
        console.warn("There was an error!", error);
      });
  };

  console.log("singleMovieData ", movie);
  return (
    <div className="container">
      {loading && <p>Loading data...</p>}
      <div className="single-block">
        <div className="image-block">
          <img src={movie?.image} alt={movie?.title} />
        </div>
        <div className="details-container">
          <h2>{movie?.title}</h2>
          <div className="date">
            <div>
              <p>{moment(movie?.release).format("YYYY")}</p>
            </div>
            <div>
              <p>{movie?.duration} min</p>
            </div>
            <div>
              <p>
                IMDb <span>{movie?.rating}</span>
              </p>
            </div>
          </div>
          <p className="description">{movie?.description}</p>
          <div className="details">
            <div className="names">
              <p>Country:</p>
              <p>Genre:</p>
              <p>Actors:</p>
              <p>Budget:</p>
            </div>

            <div className="dates">
              <p>{movie?.country}</p>
              <p>{movie?.genres.join(", ")}</p>
              <p>{movie?.actors.join(", ")}</p>
              <p>${movie?.budget} mln</p>
            </div>
          </div>
        </div>
      </div>

      <div className="reviews-container">
        <h2>Reviews</h2>
        <div className="button">
          <button onClick={() => setOpen(!open)}>
            <p>Add review</p>
          </button>
        </div>
        <div>{open && reviewContent}</div>
        {movie &&
          movie?.reviews
            .map((review) => (
              <div className="review" key={review.content}>
                <div className="user">
                  <p>{review.user}</p>
                  <div>
                    <span className="dot"></span>
                    <p>{moment(review.date).format("LLL")}</p>
                  </div>
                </div>
                <div className="text">
                  <p>{review.content}</p>
                </div>
                {userRole === "admin" ? (
                  <div>
                    {/* <button>
                      <Link to={`/update/${review.id}`}>Update</Link>
                    </button> */}
                    <Button
                      variant="contained"
                      color="error"
                      sx={{ mt: 1 }}
                      onClick={() => deleteReview(review.id)}
                    >
                      Delete
                    </Button>
                  </div>
                ) : (
                  ""
                )}
              </div>
            ))
            .reverse()}
      </div>
      {error && JSON.stringify(error)}
    </div>
  );
}
