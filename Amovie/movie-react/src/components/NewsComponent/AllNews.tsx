import "../../styles/lastNews.scss";
import moment from "moment";
import { Link } from "react-router-dom";
import useFetch from "../../hooks/useFetch";
import { Box, Button, CircularProgress, Pagination } from "@mui/material";
import { useState } from "react";
import { NewsPage } from "../../Types/Types";

export default function AllNews() {
  const [page, setPage] = useState(1);

  const url = `http://localhost:7063/newspage/${page}?pageSize=${5}`;

  const { data, error, loading, refetch } = useFetch<NewsPage>(url);
  console.log("data ", data);

  const userRole = localStorage.getItem("role");
  const deleteN = async (id: number) => {
    const url = `http://localhost:7063/api/deletenews/${id}`;
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

  return (
    <div>
      <div className="news-block">
        <div className="title-block">
          <p className="title">News</p>

          <div className="blue-line">
            <div> </div>
          </div>
          {loading && <CircularProgress color="primary" />}
          <Pagination
            size="large"
            count={data?.pages}
            page={page}
            siblingCount={0}
            onChange={(_, page) => setPage(page)}
          />
          {userRole === "admin" ? (
            <div className="button container">
              <Link to="/addnews">
                <button>
                  <p>Add news</p>
                </button>
              </Link>
            </div>
          ) : (
            ""
          )}
        </div>
        {data?.news
          ?.map((n) => (
            <div className="container" key={n.id}>
              <div className="content-block">
                <div>
                  <Link to={`/news/${n.id}`}>
                    <div className="image-section">
                      <img src={`http://localhost:7063/images/${n.image}`} />
                    </div>
                  </Link>
                  {userRole === "admin" ? (
                    <Box
                      className="buttons"
                      sx={{ justifyContent: "center", display: "flex", mt: 1 }}
                    >
                      <Link to={`/updatenews/${n.id}`}>
                        <Button sx={{ mr: 2 }} variant="contained" color="info">
                          Update
                        </Button>
                      </Link>
                      <Button
                        sx={{ ml: 2 }}
                        onClick={() => deleteN(n.id)}
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
                <div className="text-section">
                  <h2 className="title">{n.title}</h2>
                  <p className="text">{n.content}</p>
                  <div className="info">
                    <p>
                      BY <span>{n.authorName}</span>
                    </p>
                    <p>{moment(n.date).format("MMMM d Y")}</p>
                  </div>
                </div>
              </div>
              <hr className="line" />
            </div>
          ))
          .reverse()}
        {error && JSON.stringify(error)}
      </div>
    </div>
  );
}
