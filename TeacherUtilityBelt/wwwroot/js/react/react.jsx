class CommentBox extends React.Component {
    render() {
        return (
            <div className="commentBox">Hello, world! I am a CommentBox.</div>
        );
    }
}

ReactDom.render(<CommentBox data={data} />, document.getElementById("content"));
