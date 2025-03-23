
document.addEventListener("DOMContentLoaded", () => {
  const voteButtons = document.querySelectorAll(".vote-btn");

  voteButtons.forEach((btn) => {
    btn.addEventListener("click", async () => {
      const ideaId = btn.getAttribute("data-id");
      const response = await fetch(`/api/ideas/${ideaId}`, { method: "PATCH" });

      if (response.ok) {
        btn.innerText = "Voted!";
        btn.disabled = true;
        btn.classList.add("opacity-50");
      }
    });
  });
});
