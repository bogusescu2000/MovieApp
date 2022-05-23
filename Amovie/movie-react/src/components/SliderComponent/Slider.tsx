import { Component } from "react";
import Slider from "react-slick";
import "slick-carousel/slick/slick.css";
import "slick-carousel/slick/slick-theme.css";
import { Link } from 'react-router-dom';
import "../../styles/slider.scss"

export default class SimpleSlider extends Component {
  render() {
    const settings = {
      infinite: true,
      speed: 500,
      slidesToShow: 1,
      slidesToScroll: 1,
      arrows: true,
    };
    return (
      <section className="section">
        <Slider {...settings}>
          <div>
            <div className="image-container">
            <img src={process.env.PUBLIC_URL + '/ImagesUI/Slider.png'} alt="slider"/>
              <div className="centered-text">
                <p>Avengers Final</p>
              </div>
              <div className="centered-button">
              <Link to={`/movies`}>
                <button type="button"> View all movies</button>
                </Link>
              </div>
            </div>
          </div>
          <div>
            <div className="image-container">
            <img src={process.env.PUBLIC_URL + '/ImagesUI/Slider.png'} alt="slider"/>
              <div className="centered-text">
                <p>Avengers Endgame</p>
              </div>
              <div className="centered-button">
                <button type="button"> View all movies</button>
              </div>
            </div>
          </div>
        </Slider>
      </section>
    );
  }
}
