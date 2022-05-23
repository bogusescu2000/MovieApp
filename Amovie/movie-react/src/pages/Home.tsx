import SimpleSlider from "../components/SliderComponent/Slider";
import LatestMovies from "../components/MovieComponent/LastMovies";
import LastNews from "../components/NewsComponent/LastNews";

export default function Home() {
  return (
    <div>
      <SimpleSlider />
      <LastNews />
      <LatestMovies />
    </div>
  );
}
