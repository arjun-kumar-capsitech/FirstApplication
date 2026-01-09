import Navbar from "../Navbar";

const Dashboard = () => {
  const currentUser = JSON.parse(localStorage.getItem("currentUser") || "{}");

  return (
    <>
      <Navbar />
      <div className="h-screen w-screen flex items-center justify-center bg-gray-100">
        <h1 className="text-3xl font-bold">
          Welcome {currentUser.username} 
        </h1>
      </div>
    </>
  );
};

export default Dashboard;
