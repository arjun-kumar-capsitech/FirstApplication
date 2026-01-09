import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import Navbar from "../Navbar";

const Login = () => {
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const navigate = useNavigate();

    useEffect(() => {
        const token = localStorage.getItem("token");
        if (token) navigate("/dash2", { replace: true });
    }, [navigate]);

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();

        if (!username || !password) {
            alert("Please enter username and password");
            return;
        }

        try {
            const response = await fetch("http://localhost:5176/api/Login", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify({ username, password }),
            });

            if (!response.ok) {
                alert("Invalid username or password");
                return;
            }

            const data = await response.json();

            localStorage.setItem("token", data.token);
            localStorage.setItem("currentUser", JSON.stringify({ username: data.username }));

            const secureResponse = await fetch("http://localhost:5176/api/Secure/data", {
                headers: {
                    Authorization: `Bearer ${data.token}`,
                },
            });

            if (secureResponse.ok) {
                const secureData = await secureResponse.json();
                console.log("Secure Data:", secureData);
            }

            const regResponse = await fetch("http://localhost:5176/api/Reg", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    Authorization: `Bearer ${data.token}`,
                },
                body: JSON.stringify({
                    username: data.username,
                    email: data.email || "",
                    password,
                    confirmPassword: password,
                    role: "User",
                }),
            });

            if (regResponse.ok) {
                const regData = await regResponse.json();
                console.log("Reg Data:", regData);
            }

            navigate("/dash2");
        } catch (error) {
            alert("Server error, please try again");
        }
    };

    return (
        <>
            <Navbar />
            <div className="h-screen w-screen flex items-center justify-center bg-fixed bg-cover bg-center">
                <div className="bg-white backdrop-blur-md p-5 rounded-lg shadow-xl w-[390px]">
                    <h2 className="text-2xl font-bold mb-6 text-center">Admin Login</h2>
                    <form onSubmit={handleSubmit} className="space-y-4">
                        <div>
                            <label className="block mb-1 text-gray-700">Username</label>
                            <input
                                type="text"
                                value={username}
                                onChange={(e) => setUsername(e.target.value)}
                                className="w-full px-3 py-2 border rounded-md focus:ring-2 focus:ring-blue-500"
                                placeholder="Enter admin username"
                            />
                        </div>
                        <div>
                            <label className="block mb-1 text-gray-700">Password</label>
                            <input
                                type="password"
                                value={password}
                                onChange={(e) => setPassword(e.target.value)}
                                className="w-full px-3 py-2 border rounded-md focus:ring-2 focus:ring-blue-500"
                                placeholder="Enter admin password"
                            />
                        </div>

                        <div className="flex space-x-2 mt-4">
                            <button
                                type="submit"
                                className="w-1/2 bg-blue-600 text-white py-2 rounded hover:bg-blue-700"
                            >
                                Login
                            </button>
                            <a
                                href="/reg"
                                className="w-1/2 bg-blue-600 text-white py-2 rounded hover:bg-blue-700 text-center flex items-center justify-center"
                            >
                                Register
                            </a>
                        </div>

                    </form>
                </div>
            </div>
        </>
    );
};

export default Login;
