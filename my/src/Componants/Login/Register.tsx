import { useState } from "react";
import { useNavigate } from "react-router-dom";
import Navbar from "../Navbar";

const Register = () => {
    const [username, setUsername] = useState("");
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [confirmPassword, setConfirmPassword] = useState("");
    const navigate = useNavigate();

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();

        if (!username || !email || !password || !confirmPassword) {
            alert("Please fill all fields");
            return;
        }

        if (password !== confirmPassword) {
            alert("Passwords do not match");
            return;
        }

        try {
            const response = await fetch("http://localhost:5176/api/Reg", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify({ username, email, password, confirmPassword }),
            });

            if (!response.ok) {
                alert("Registration failed");
                return;
            }

            const data = await response.json();

            localStorage.setItem(
                "currentUser",
                JSON.stringify({
                    username: data.username,
                    email: data.email,
                })
            );

            alert("Registration successful!");
            navigate("/login");
        } catch (error) {
            alert("Server error, please try again");
        }
    };

    return (
        <>
            <Navbar />
            <div className="h-screen w-screen flex items-center justify-center bg-fixed bg-cover bg-center">
                <div className="bg-white backdrop-blur-md p-5 rounded-lg shadow-xl w-[420px]">
                    <h2 className="text-2xl font-bold mb-6 text-center">Register</h2>
                    <form onSubmit={handleSubmit} className="space-y-4">
                        <div>
                            <label className="block mb-1 text-gray-700">Username</label>
                            <input
                                type="text"
                                value={username}
                                onChange={(e) => setUsername(e.target.value)}
                                className="w-full px-3 py-2 border rounded-md focus:ring-2 focus:ring-blue-500"
                                placeholder="Enter your username"
                            />
                        </div>

                        <div>
                            <label className="block mb-1 text-gray-700">Email</label>
                            <input
                                type="email"
                                value={email}
                                onChange={(e) => setEmail(e.target.value)}
                                className="w-full px-3 py-2 border rounded-md focus:ring-2 focus:ring-blue-500"
                                placeholder="Enter your email"
                            />
                        </div>

                        <div>
                            <label className="block mb-1 text-gray-700">Password</label>
                            <input
                                type="password"
                                value={password}
                                onChange={(e) => setPassword(e.target.value)}
                                className="w-full px-3 py-2 border rounded-md focus:ring-2 focus:ring-blue-500"
                                placeholder="Enter your password"
                            />
                        </div>

                        <div>
                            <label className="block mb-1 text-gray-700">Confirm Password</label>
                            <input
                                type="password"
                                value={confirmPassword}
                                onChange={(e) => setConfirmPassword(e.target.value)}
                                className="w-full px-3 py-2 border rounded-md focus:ring-2 focus:ring-blue-500"
                                placeholder="Confirm your password"
                            />
                        </div>

                        <div className="flex space-x-2 mt-4">
                            <button
                                type="submit"
                                className="w-1/2 bg-blue-600 text-white py-2 rounded hover:bg-blue-700"
                            >
                                Register
                            </button>
                            <a
                                href="/login"
                                className="w-1/2 bg-blue-600 text-white py-2 rounded hover:bg-blue-700 text-center flex items-center justify-center"
                            >
                                Login
                            </a>
                        </div>
                    </form>
                </div>
            </div>
        </>
    );
};

export default Register;
