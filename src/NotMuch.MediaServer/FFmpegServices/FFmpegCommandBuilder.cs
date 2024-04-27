using System;
using System.Collections.Generic;
using System.Text;

namespace NotMuch.MediaServer.FFmpegServices;


public enum FFmpegCommandType
{
    Convert,
    ExtractAudio,
    ExtractImage
}

public enum FFmpegOutputFormat
{
    Mp3,
    Wav,
    Aac,
    Flac,
    Ogg,
    M4a,
    Webm,
    Avi,
    Mp4,
    Mkv
}

public enum FFmpegVideoCodec
{
    H264,
    H265,
    Vp8,
    Vp9,
    Av1
}

public enum FFmpegVideoBitrate
{
    _32k,
    _64k,
    _96k,
    _128k,
    _192k,
    _256k,
    _320k
}

public enum FFmpegVideoResolution
{
    _240p,
    _360p,
    _480p,
    _720p,
    _1080p,
    _1440p,
    _2160p
}

public enum FFmpegVideoAspectRatio
{
    _4_3,
    _16_9,
    _21_9
}

public enum FFmpegVideoFrameRate
{
    _24,
    _30,
    _60,
    _120
}

public enum FFmpegVideoQuality
{
    Low,
    Medium,
    High
}

public enum FFmpegVideoCodecProfile
{
    Baseline,
    Main,
    High
}

public enum FFmpegVideoCodecLevel
{
    _1_0,
    _1_1,
    _1_2,
    _1_3,
    _2_0,
    _2_1,
    _2_2,
    _3_0,
    _3_1,
    _3_2,
    _4_0,
    _4_1,
    _4_2,
    _5_0,
    _5_1,
    _5_2,
    _6_0,
    _6_1,
    _6_2
}

public enum FFmpegVideoCodecPreset
{
    UltraFast,
    SuperFast,
    VeryFast,
    Faster,
    Fast,
    Medium,
    Slow,
    Slower,
    VerySlow
}

public enum FFmpegVideoCodecTune
{
    Film,
    Animation,
    Grain,
    StillImage,
    Psnr,
    Ssim,
    FastDecode,
    ZeroLatency
}

public enum FFmpegVideoCodecProfileLevel
{
    Baseline_1_0,
    Baseline_1_1,
    Baseline_1_2,
    Baseline_1_3,
    Main_1_0,
    Main_1_1,
    Main_1_2,
    Main_1_3,
    Main_2_0,
    Main_2_1,
    Main_2_2,
    Main_3_0,
    Main_3_1,
    Main_3_2,
    High_1_0,
    High_1_1,
    High_1_2,
    High_1_3,
    High_2_0,
    High_2_1,
    High_2_2,
    High_3_0,
    High_3_1,
    High_3_2
}

public enum FFmpegAudioCodec
{
    Aac,
    Mp3,
    Flac,
    Opus,
    Vorbis
}

public enum FFmpegAudioBitrate
{
    _32k,
    _64k,
    _96k,
    _128k,
    _192k,
    _256k,
    _320k
}

public enum FFmpegAudioSampleRate
{
    _8000,
    _11025,
    _12000,
    _16000,
    _22050,
    _24000,
    _32000,
    _44100,
    _48000,
    _64000,
    _88200,
    _96000
}

public enum FFmpegAudioChannel
{
    Mono,
    Stereo,
    _5_1,
    _7_1
}

public enum FFmpegAudioQuality
{
    Low,
    Medium,
    High
}

public enum FFmpegImageFormat
{
    Jpg,
    Png,
    Gif,
    Bmp,
    Tiff
}

public enum FFmpegImageQuality
{
    Low,
    Medium,
    High
}

public enum FFmpegImageResolution
{
    _240p,
    _360p,
    _480p,
    _720p,
    _1080p,
    _1440p,
    _2160p
}

public enum FFmpegImageAspectRatio
{
    _4_3,
    _16_9,
    _21_9
}

public enum FFmpegImageFrameRate
{
    _24,
    _30,
    _60,
    _120
}

public enum FFmpegImageCodec
{
    Jpeg,
    Png,
    Gif,
    Bmp,
    Tiff
}

public enum FFmpegImageBitrate
{
    _32k,
    _64k,
    _96k,
    _128k,
    _192k,
    _256k,
    _320k
}
internal class FFmpegCommandBuilder
{
    public string ToString()
    {
        throw new NotImplementedException();
    }
}
