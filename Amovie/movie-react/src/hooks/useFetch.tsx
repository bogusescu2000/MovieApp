import { useEffect, useState } from "react";

export default function useFetch<T = unknown>(url: string) {
  const [data, setData] = useState<T | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  const refetch = async () => {
    const response = await fetch(url);
    setLoading(false);
    var result;
    if (isJson(response)) {
      result = await response.json();
    }
    if (response.status === 404) {
      result = { error404: "Resourse not found" };
    } else {
      result = result || { error: "Somthing went wrong" };
    }

    if (response.status >= 200 && response.status < 300) {
      setData(result);
    } else {
      setError(result);
    }
  }

  useEffect(() => {
    async function fetchPosts() {
      const response = await fetch(url);
      setLoading(false);
      var result;
      if (isJson(response)) {
        result = await response.json();
      }
      if (response.status === 404) {
        result = { error404: "Resourse not found" };
      } else {
        result = result || { error: "Somthing went wrong" };
      }

      if (response.status >= 200 && response.status < 300) {
        setData(result);
      } else {
        setError(result);
      }
    }
    fetchPosts();
  }, [url]);

  return { data, loading, error, refetch, setData};
}

function isJson(response: Response) {
  return response.headers.get("Content-type")?.includes("json");
}
