import { useNavigate } from "react-router-dom";

export default function Index() {
  const navigate = useNavigate();

  const handleStart = () => {
    navigate("/reg");
  };

  return (
    <div className="min-h-screen flex flex-col items-center justify-center bg-gray-900 via-blue-900 to-gray-900 text-white p-4">
      <h1 className="text-3xl sm:text-5xl font-bold mb-6 text-center">
        Project Manager
      </h1>
      <button
        onClick={handleStart}
        className="bg-blue-600 px-6 sm:px-8 py-2 sm:py-3 rounded text-lg sm:text-xl hover:bg-blue-700 transition-colors duration-300 font-semibold shadow-lg"
      >
        Start
      </button>
    </div>
  );
}
