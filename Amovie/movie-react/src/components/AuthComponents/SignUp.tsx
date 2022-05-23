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
import Container from "@mui/material/Container";
import { yupResolver } from "@hookform/resolvers/yup";
import { registerSchema } from "../../validations/registerValidation";
import { Redirect } from "react-router-dom";
import { useState } from "react";
import { RegisterType } from "../../Types/Types";

export default function SignUp() {
  const [redirect, setRedirect] = useState(false);
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<RegisterType>({ resolver: yupResolver(registerSchema) });

  const onSubmit = async (values: RegisterType) => {
    console.log(values);

    const url = "http://localhost:7063/api/register";
    const data = {
      name: values.name,
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
      });
      const json = (await response).json();
      console.log("Succes:", JSON.stringify(json));
    } catch (error) {
      console.error("Error:", error);
    }
    setRedirect(true);
  };

  if (redirect) {
    return <Redirect to="/signin" />;
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
          Sign up
        </Typography>
        <Box component="form" onSubmit={handleSubmit(onSubmit)} sx={{ mt: 3 }}>
          <Grid container spacing={2}>
            <Grid item xs={12}>
              <TextField
                fullWidth
                id="name"
                label="Name"
                {...register("name", {})}
              />
              {errors.name && (
                <Typography style={{ color: "#F95252" }}>
                  * {errors.name.message}
                </Typography>
              )}
            </Grid>
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
            <Grid item xs={12}>
              <TextField
                fullWidth
                label="Confirm password"
                id="password2"
                type="password"
                {...register("confirmPassword")}
              />
              {errors.confirmPassword && (
                <Typography style={{ color: "#F95252" }}>
                  * {errors.confirmPassword.message}
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
            Sign Up
          </Button>
          <Grid container justifyContent="flex-end">
            <Grid item>
              <Link to="/signin"> Already have an account? Sign in </Link>
            </Grid>
          </Grid>
        </Box>
      </Box>
    </Container>
  );
}
