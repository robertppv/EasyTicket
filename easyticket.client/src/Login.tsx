import  { useState } from "react";
import { useNavigate } from "react-router-dom";


export default function Login() {
    const [email, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const navigate = useNavigate();
    
    const handleLogin = async () => {
        const res = await fetch("api/Auth/login", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ email, password }),
        });

        if (res.ok) {
            
            const data = await res.json();
            alert(data);
            localStorage.setItem("token", data.token);
            navigate("/dashboard");
            
        } else {
            alert("Invalid credentials");
        }
    };

    return (
        <div>
            <h2>Login</h2>
        <input placeholder="Username" onChange={e => setUsername(e.target.value)} />
    <input type="password" placeholder="Password" onChange={e => setPassword(e.target.value)} />
    <button onClick={handleLogin}>Login</button>
        </div>
);
}