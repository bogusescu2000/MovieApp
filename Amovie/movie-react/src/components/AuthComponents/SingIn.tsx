import * as React from "react";
import Container from "@mui/material/Container";
import {
  Avatar,
  Box,
  Button,
  Grid,
  TextField,
  Typography,
} from "@mui/material";
import { Link } from "react-router-dom";
import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import { loginSchema } from "../../validations/loginValidation";
import { useState } from "react";
import { Redirect } from "react-router-dom";
import { UserContext } from "../../providers/UserProvider";
import { LoginType } from "../../Types/Types";

export default function SignIn() {
  const [redirect, setRedirect] = useState(false);
  const { user, setUser } = React.useContext(UserContext);
  
  const methods = useForm<LoginType>({ resolver: yupResolver(loginSchema) });

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = methods;

  const onSubmit = async (values: LoginType) => {
    const url = "http://localhost:7063/api/login";
    const data = {
      email: values.email,
      password: values.password,
    };

    try {
      const response = await fetch(url, {
        method: "POST",
        body: JSON.stringify(data),
        headers: {
          "Content-Type": "application/json",
        },
        credentials: "include",
      });
      var result = await response.json();
      console.log("Succes:", JSON.stringify(result));
    } catch (error) {
      console.error("Error:", error);
    }


    //   function parseJwt (token:string) {
    //     var atob = require('atob');
    //     var base64Url = token.split('.')[1];
    //     var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    //     var jsonPayload = decodeURIComponent(atob(base64).split('').map(function(c:string) {
    //         return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    //     }).join(''));

    //     return jsonPayload;
    // };
    //  var jwt = require("jsonwebtoken");
    //   const jwtToken = result.jwt;
    //   var decode1 = jwt.decode(jwtToken);

    //   console.log("decode "+ decode1);

    localStorage.setItem('name', result.user.name);
    localStorage.setItem('role', result.user.userRole);

    setUser({ name: result.user.name, role: result.user.userRole });
    setRedirect(true);
  };

  if (redirect) {
    return <Redirect to="/" />;
  }

  return (
    <Container component="main" maxWidth="xs">
      <Box
        sx={{
          marginTop: 8,
          marginBottom: 8,
          display: "flex",
          flexDirection: "column",
          alignItems: "center",
        }}
      >
        <Avatar sx={{ m: 1, bgcolor: "primary.main" }}></Avatar>
        <Typography component="h1" variant="h5" sx={{ fontWeight: "bold" }}>
          Sign In
        </Typography>
        <Box component="form" onSubmit={handleSubmit(onSubmit)} sx={{ mt: 3 }}>
          <Grid container spacing={2}>
            <Grid item xs={12}>
              <TextField
                fullWidth
                id="email"
                label="Email Address"
                {...register("email", {})}
              />
              {errors.email && (
                <Typography style={{ color: "#F95252" }}>
                  * {errors.email.message}
                </Typography>
              )}
            </Grid>
            <Grid item xs={12}>
              <TextField
                fullWidth
                label="Password"
                id="password1"
                type="password"
                {...register("password", {})}
              />
              {errors.password && (
                <Typography style={{ color: "#F95252" }}>
                  * {errors.password.message}
                </Typography>
              )}
            </Grid>
          </Grid>
          <Button
            type="submit"
            fullWidth
            variant="contained"
            sx={{ mt: 3, mb: 2 }}
          >
            Sign In
          </Button>
          <Grid container justifyContent="flex-end">
            <Grid item>
              <Link to="/signup"> Don't have an account? Sign Up </Link>
            </Grid>
          </Grid>
        </Box>
      </Box>
    </Container>
  );
}
