const useFetch = (url) => {
    const [data, setData] = React.useState(null);
    const [isLoading, setIsLoading] = React.useState(true);
    const [error, setError] = React.useState(null);

    React.useEffect(() => {
        setTimeout(() => {
            fetch(url)
                .then((response) => {
                    if (!response.ok) {
                        throw Error(
                            "Could not fetch the data for that resource..."
                        );
                    }
                    return response.json();
                })
                .then((data) => {
                    setIsLoading(false);
                    setError(null);
                    setData(data);
                    console.log(data);
                })
                .catch((error) => {
                    if (error.name === "AbortError") {
                        console.log("Fetch aborted.");
                    } else {
                        setIsLoading(false);
                        setError(error.message);
                    }
                });
        }, 500);
    }, [url]);

    return { data, isLoading, error };
};

export default useFetch;
