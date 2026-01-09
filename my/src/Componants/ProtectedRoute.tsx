import { Navigate } from "react-router-dom";
import type { ReactNode } from "react";

interface Props {
  children: ReactNode;
}

function ProtectedRoute({ children }: Props) {
  const isAuth = localStorage.getItem("token");
  return isAuth ? children : <Navigate to="/login" replace />;
}

export default ProtectedRoute;
  