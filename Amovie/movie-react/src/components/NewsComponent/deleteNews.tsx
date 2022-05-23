import { useEffect, useState } from "react";
import useFetch from "../../hooks/useFetch";
import { NewsPage, NewsType } from "../../Types/Types";

export default async function deleteNews(id:number) {
  const url = `http://localhost:7063/api/deletenews/${id}`;
  await fetch(url, {
    method: "DELETE",
  })
    .then(async (response) => {
      if (response.ok) {

      }
    })
    .catch((error) => {
      console.warn("There was an error!", error);
    });
}

  // var message = prompt("Are you sure?");
    // window.location.reload();
