import NavbarComponent from "./components/NavComponent/Navbar";
import Footer from "./components/FooterComponent/Footer";
import Home from "./pages/Home";
import Movies from "./pages/Movies";
import News from "./pages/News";
import SingleMovie from "./pages/SingleMovie";
import SingleNews from "./pages/SingleNews";

import { Route } from "react-router";
import { BrowserRouter as Router } from "react-router-dom";

import "./styles/main.scss";
import SignIn from "./components/AuthComponents/SingIn";
import SignUp from "./components/AuthComponents/SignUp";
import AddNews from "./components/NewsComponent/AddNews";
import UpdateNews from "./components/NewsComponent/UpdateNews";
import AddMovie from "./components/MovieComponent/AddMovie";

function App() {
  // const {user, setUser} = useContext(UserContext);

  // useEffect(() => {
  //   (async () => {
  //     const response = await fetch("http://localhost:7063/api/user", {
  //       headers: { "Content-Type": "application/json" },
  //       credentials: "include",
  //     });
  //     const content = await response.json();
  //     setUser({name: content.name});
  //   })();
  // }, []);
  // console.log("user App => " + user?.name);

  return (
    <div>
      <Router>
        <NavbarComponent />
        <Route exact path="/" component={Home} />
        <Route exact path="/movies" component={Movies} />
        <Route exact path="/news" component={News} />
        <Route path="/movies/:id" component={SingleMovie} />
        <Route path="/news/:id" component={SingleNews} />
        <Route path="/addnews" component={AddNews} />
        <Route path="/addmovie" component={AddMovie} />
        <Route path="/updatenews/:id" component={UpdateNews} />
        <Route path="/signin" component={SignIn} />
        <Route path="/signup" component={SignUp} />
        <Footer />
      </Router>
    </div>
  );
}

export default App;
