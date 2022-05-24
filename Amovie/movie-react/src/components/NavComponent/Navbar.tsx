import "../../styles/navbar.scss";
import { Link, NavLink } from "react-router-dom";
import { useContext, useState } from "react";
import {
  AppBar,
  Box,
  IconButton,
  Menu,
  MenuItem,
  Toolbar,
  Typography,
} from "@mui/material";
import { Container, Button } from "@mui/material";
import React from "react";
import MenuIcon from "@mui/icons-material/Menu";
import PersonIcon from "@mui/icons-material/Person";
import { UserContext } from "../../providers/UserProvider";

const ResponsiveAppBar = () => {
  const [anchorElNav, setAnchorElNav] = useState<null | HTMLElement>(null);

  const { user, setUser } = useContext(UserContext);

  const handleOpenNavMenu = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorElNav(event.currentTarget);
  };

  const handleCloseNavMenu = () => {
    setAnchorElNav(null);
  };

  const logout = () => {
    const url = "http://localhost:7063/api/logout";
    fetch(url, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      credentials: "include",
    });
    window.localStorage.removeItem("name");
    window.localStorage.removeItem("role");
    setUser({ name: "", role: "" });
  };

  let currentUser = localStorage.getItem("name");
  let menu;
  if (user?.name === "") {
    menu = (
      <Box>
        <NavLink activeClassName="is-active" to="/signin">
          <Button className="button">Sign In</Button>
        </NavLink>
        <NavLink activeClassName="is-active" to="/signup">
          <Button className="button">Sign Up</Button>
        </NavLink>
      </Box>
    );
  } else {
    menu = (
      <Box>
        <Button
          sx={{ color: "#42474D ", textTransform: "none", fontSize: "16px" }}
        >
          Hello {currentUser}
        </Button>
        <NavLink activeClassName="is-active" to="/signin" onClick={logout}>
          <Button className="button">Logout</Button>
        </NavLink>
      </Box>
    );
  }

  return (
    <AppBar position="static">
      <Container>
        <Toolbar disableGutters>
          <Typography
            variant="h6"
            noWrap
            component="div"
            sx={{ mr: 2, display: { xs: "none", md: "flex" } }}
          >
            <Link to="/">
              <img
                src={process.env.PUBLIC_URL + "/ImagesUI/Logo.svg"}
                alt="logo"
              />
            </Link>
          </Typography>

          <Box sx={{ flexGrow: 1, display: { xs: "flex", md: "none" } }}>
            <IconButton
              size="large"
              aria-controls="menu-appbar"
              aria-haspopup="true"
              onClick={handleOpenNavMenu}
              color="primary"
            >
              <MenuIcon />
            </IconButton>
            <Menu
              id="menu-appbar"
              anchorEl={anchorElNav}
              anchorOrigin={{
                vertical: "bottom",
                horizontal: "left",
              }}
              keepMounted
              transformOrigin={{
                vertical: "top",
                horizontal: "left",
              }}
              open={Boolean(anchorElNav)}
              onClose={handleCloseNavMenu}
              sx={{ display: { xs: "block", md: "none" } }}
            >
              <MenuItem onClick={handleCloseNavMenu}>
                <NavLink activeClassName="is-active" to="/movies">
                  <Button className="button">Movies</Button>
                </NavLink>
              </MenuItem>
              <MenuItem onClick={handleCloseNavMenu}>
                <NavLink activeClassName="is-active" to="/news">
                  <Button className="button">News</Button>
                </NavLink>
              </MenuItem>

              <MenuItem onClick={handleCloseNavMenu}>
                <NavLink activeClassName="is-active" to="/signin">
                  <Button className="button">Sign In</Button>
                </NavLink>
              </MenuItem>
              <MenuItem onClick={handleCloseNavMenu}>
                <NavLink activeClassName="is-active" to="/signup">
                  <Button className="button">Sign Up</Button>
                </NavLink>
              </MenuItem>
            </Menu>
          </Box>
          <Typography
            variant="h6"
            noWrap
            component="div"
            sx={{ flexGrow: 2, display: { xs: "flex", md: "none" } }}
          >
            <Link to="/">
              <img
                src={process.env.PUBLIC_URL + "/ImagesUI/Logo.svg"}
                alt="logo"
              />
            </Link>
          </Typography>
          <Box className="box">
            <NavLink activeClassName="is-active" to="/movies">
              <Button className="button">Movies</Button>
            </NavLink>
            <NavLink activeClassName="is-active" to="/news">
              <Button className="button">News</Button>
            </NavLink>
          </Box>
          <Box
            className="box"
            sx={{ flexGrow: 5, display: { xs: "none", md: "flex" } }}
          >
            <PersonIcon />
            <Box>{menu}</Box>
          </Box>
        </Toolbar>
      </Container>
    </AppBar>
  );
};
export default ResponsiveAppBar;
