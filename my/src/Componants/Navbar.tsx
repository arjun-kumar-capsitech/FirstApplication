import { Link, useLocation, useNavigate } from "react-router-dom";
import { LogIn, LogOutIcon } from "lucide-react";

function Navbar() {
  const location = useLocation();
  const navigate = useNavigate();
  const isLoggedIn = !!localStorage.getItem("token");

  const isActive = (path: string) =>
    location.pathname === path ? "text-blue-500 font-bold" : "text-white";

  const handleLogout = () => {
    localStorage.removeItem("token");
    localStorage.removeItem("currentUser");
    navigate("/login", { replace: true });
  };

  return (
    <nav className="bg-gray-900 text-white px-6 py-3 flex justify-between items-center">
      <h1 className="text-2xl font-bold">Project Manager</h1>
      <div className="flex gap-6 text-xl">
        <Link to="/login" className={isActive("/dash2")}>Dashboard</Link>
        <Link to="/login">Users</Link>
        <Link to="/login">Projects</Link>
        <Link to="/login" >Tasks</Link>
      </div>

      {!isLoggedIn && (
        <button>
          <Link
            to="/login"
            className="bg-sky-600 px-4 py-2 rounded flex items-center gap-2 hover:bg-sky-700"
          >  
           <LogIn />
            Login
          </Link>
        </button>
      )}
      {isLoggedIn && (
        <button
          onClick={handleLogout}
          className="bg-red-600 px-4 py-2 rounded hover:bg-red-700 flex items-center gap-2"
        >
          <LogOutIcon />
          Logout
        </button>
      )}
    </nav>
  );
}

export default Navbar;
