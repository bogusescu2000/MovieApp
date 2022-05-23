import { Link } from "react-router-dom";
import "../../styles/footer.scss";

export default function Footer() {
  return (
    <div className="footer-block">
      <div className="logo">
        <Link to="/">
          <img src={process.env.PUBLIC_URL + "/ImagesUI/Logo.svg"} alt="logo" />
        </Link>
      </div>
      <div className="pages">
        <p><Link to="/movies">Movies</Link></p>
        <p><Link to="/news">News</Link></p>
        <p><Link to="/signin">Sign In</Link></p>
        <p><Link to="/signup">Sign Up</Link></p>
        <a href="#">
          <img
            src={process.env.PUBLIC_URL + "/ImagesUI/instagram.svg"}
            alt="instagram"
          />
        </a>
        <a href="#">
          <img
            src={process.env.PUBLIC_URL + "/ImagesUI/facebook.svg"}
            alt="facebook"
          />
        </a>
        <a href="#">
          <img
            src={process.env.PUBLIC_URL + "/ImagesUI/twitter.svg"}
            alt="twitter"
          />
        </a>
      </div>
      <div className="copyright">
        <p>Copyright 2022 Amovie.com </p>
      </div>
    </div>
  );
}
