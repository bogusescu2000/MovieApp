import "../../styles/navbar.scss";
import { Link } from "react-router-dom";
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

  let menu;
  let currentUser = localStorage.getItem("name");
  if (user?.name === "") {
    menu = (
      <Box>
        <Link to="/signin">
          <Button>Sign In</Button>
        </Link>
        <Link to="/signup">
          <Button>Sign Up</Button>
        </Link>
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
        <Link to="/signin" onClick={logout}>
          <Button>Logout</Button>
        </Link>
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
                <Link to="/movies">
                  <Button>Movies</Button>
                </Link>
              </MenuItem>
              <MenuItem onClick={handleCloseNavMenu}>
                <Link to="/news">
                  <Button>News</Button>
                </Link>
              </MenuItem>

              <MenuItem onClick={handleCloseNavMenu}>
                <Link to="/signin">
                  <Button>Sign In</Button>
                </Link>
              </MenuItem>
              <MenuItem onClick={handleCloseNavMenu}>
                <Link to="/signup">
                  <Button>Sign Up</Button>
                </Link>
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
            <Link to="/movies">
              <Button>Movies</Button>
            </Link>
            <Link to="/news">
              <Button>News</Button>
            </Link>
          </Box>
          <Box
            className="box"
            sx={{ flexGrow: 5, display: { xs: "none", md: "flex" } }}
          >
            <PersonIcon />
            <Box>
            {menu}
            </Box>
          </Box>
        </Toolbar>
      </Container>
    </AppBar>
  );
};
export default ResponsiveAppBar;
