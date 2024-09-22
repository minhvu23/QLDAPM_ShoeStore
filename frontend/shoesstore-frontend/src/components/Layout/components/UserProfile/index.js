import React, { useEffect, useState } from "react";

const UserProfile = () => {
  const [user, setUser] = useState(null);

  useEffect(() => {
    // Retrieve user data from localStorage
    const storedUser = localStorage.getItem("user");
    if (storedUser) {
      setUser(JSON.parse(storedUser));
    }
  }, []);

  if (!user) {
    return <p>No user information available. Please log in.</p>;
  }

  return (
    <div>
      <h2>Welcome, {user.username}!</h2>
      <p>User ID: {user.userId}</p>
      <p>Email: {user.email}</p>
      {/* Display other user info */}
    </div>
  );
};

export default UserProfile;
